using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Anotar.NLog;
using Disruptor;
using QuantEdge.Lib.Common;
using QuantEdge.Lib.Interface;
using QuantEdge.Lib.Utils;
using QuantEdge.Message.Common;
using QuantEdge.Message.Controler;
using QuantEdge.Message.Event;
using QuantEdge.Message.Response;

namespace QuantEdge.Lib.Sequencer
{
    internal sealed class EventUnMarshaler : IEventHandler<ReceiveData>
    {
        private readonly Dictionary<string, PartialMessageStorage> _listPending;
        private readonly bool _isWorker;
        private readonly bool _isBStarMode;
        private IProcess _processor;
        
        internal EventUnMarshaler(IProcess processor, bool isWorker = false, bool isBStarMode = false)
        {
            _processor = processor;
            _isBStarMode = isBStarMode;
            _isWorker = isWorker;
            _listPending = new Dictionary<string, PartialMessageStorage>();
        }


        public void OnEvent(ReceiveData data, long sequence, bool endOfBatch)
        {
            var messageEvent = data;
            if (messageEvent != null)
            {
                if (messageEvent.Message is IControler || messageEvent.RawMsg == null)
                    return; //neu msg chua dc hoan thien thi ko thuc hien giai ma 

                IMessage bizData = null;
                try
                {
                    string rawStr = EncodingUtils.GetString(messageEvent.RawMsg);                   
                    bizData = JsonUtils.DeserializeMessage(rawStr);
                    if(Endpoint.IsWriteLogInfo)
                        LogTo.Info(rawStr); //Ghi toan bo thong tin gia nhan duoc
                    if (Endpoint.IsWriteLog && bizData != null)
                    {
                        #region Xu ly luu thong tin log msg
                        try
                        {
                            var msgNameBiz = bizData.GetType().ToString();
                            decimal sizeBiz = (decimal)messageEvent.RawMsg.Length / (1024 * 1024);
                            if (!Endpoint.DicMessageCount.ContainsKey(msgNameBiz))
                            {
                                Endpoint.DicMessageCount[msgNameBiz] = 0;
                                Endpoint.DicMessageSize[msgNameBiz] = 0;
                            }
                            Endpoint.DicMessageCount[msgNameBiz] += 1;
                            Endpoint.DicMessageSize[msgNameBiz] += sizeBiz;

                            if (!Endpoint.DicMessageCountFull.ContainsKey(msgNameBiz))
                            {
                                Endpoint.DicMessageCountFull[msgNameBiz] = 0;
                                Endpoint.DicMessageSizeFull[msgNameBiz] = 0;
                            }
                            Endpoint.DicMessageCountFull[msgNameBiz] += 1;
                            Endpoint.DicMessageSizeFull[msgNameBiz] += sizeBiz;

                            // sau 5p se ghi log tong so message gui di
                            var timeCheck = (DateTime.Now - Endpoint.LastCheckCountMsg).TotalSeconds;
                            if (timeCheck > Endpoint.TimeWriteLogMsgReceived)
                            {
                                // sau 5 phut ghi log 1 lan
                                // sau 5 moi ghi log
                                decimal countAll = 0;
                                decimal countSizeAll = 0;
                                decimal count5MinAll = 0;
                                decimal count5MinSizeAll = 0;
                                StringBuilder strDetail = new StringBuilder();
                                var dicSort = Endpoint.DicMessageSizeFull.OrderBy(s => s.Key);
                                foreach (var keyValue in dicSort)
                                {
                                    string msgName = keyValue.Key;
                                    decimal sizeForMsg = keyValue.Value;
                                    decimal countForMsg = Endpoint.DicMessageCountFull[msgName];
                                    decimal count5Min = 0;
                                    if (Endpoint.DicMessageCount.ContainsKey(msgName))
                                        count5Min = Endpoint.DicMessageCount[msgName];

                                    decimal byte5Min = 0;
                                    if (Endpoint.DicMessageSize.ContainsKey(msgName))
                                        byte5Min = Endpoint.DicMessageSize[msgName];

                                    decimal avg5Min = count5Min / Endpoint.TimeWriteLogMsgReceived;
                                    countAll += countForMsg;
                                    countSizeAll += sizeForMsg;
                                    count5MinAll += count5Min;
                                    count5MinSizeAll += byte5Min;

                                    strDetail.AppendLine(GetLog(msgName, countForMsg, sizeForMsg, count5Min, byte5Min, avg5Min));
                                }
                                decimal avgAll5Min = count5MinAll / Endpoint.TimeWriteLogMsgReceived;
                                var textLogFull = string.Format(
                                    "Total message = {0} with size = {1} MB; In 5min count = {2} and size = {3} MB; avg 5 min = {4} msg/s",
                                    countAll, countSizeAll.ToString("#,##0.0000"), count5MinAll, count5MinSizeAll.ToString("#,##0.0000"), avgAll5Min.ToString("#,##0.00"));
                                StringBuilder strBuild = new StringBuilder();
                                strBuild.AppendLine(string.Format("Thoi diem dang nhap {0}", Endpoint.StartTimeLogin));
                                strBuild.AppendLine(textLogFull);
                                strBuild.AppendLine("Thong tin chi tiet : ");
                                strBuild.AppendLine(strDetail.ToString());
                                LogTo.Info(strBuild.ToString());

                                Endpoint.DicMessageCount = new Dictionary<string, decimal>();
                                Endpoint.DicMessageSize = new Dictionary<string, decimal>();
                                Endpoint.LastCheckCountMsg = DateTime.Now;
                            }
                        }
                        catch (Exception ex1)
                        {
                            LogTo.Error(ex1.ToString());
                        }
                        #endregion
                    }
                }
                catch (Exception ex)
                {
                    LogTo.Error(ex.ToString());
                    _processor.Send(new JournalMessage()
                                        {
                                            Tag = messageEvent.DeliveryTag,
                                            IsAck = true,
                                        }, messageEvent.QueueType);
                    data.SetMessage(messageEvent);
                    return;
                }

                if (_isWorker)
                {
                    switch (messageEvent.QueueType)
                    {
                        case (byte)QueueType.MainQueue:
                            //main queue se co trach nhiem xu ly msg, nen no can danh seq cho cac msg ma no xu ly
                            //tang seq truoc
                            if (_isBStarMode && !bizData.IsNotPersist)
                            {
                                //cap nhat seq vao msg goc de gui di sang replicator
                                var str = JsonUtils.SerializeMessage(bizData);
                                messageEvent.RawMsg = EncodingUtils.GetBytes(str);
                            }
                            break;
                    }
                }

                if (!messageEvent.IsObsolate && bizData is PartialMessage)
                {
                    try
                    {
                        //neu day la msg partial thi thuc hien tong hop lai
                        var pendingMsg = bizData as PartialMessage;
                        //raise event change
                        _processor.ProcessControlerMsg(new NewMessageCommingEvent { Count = pendingMsg.Count, Index = pendingMsg.Index, MsgId = pendingMsg.MainRequestKey }, messageEvent.QueueType);
                        PartialMessageStorage storage;
                        if (_listPending.ContainsKey(pendingMsg.MainRequestKey))
                        {
                            storage = _listPending[pendingMsg.MainRequestKey];
                        }
                        else
                        {
                            storage = new PartialMessageStorage(pendingMsg.Count);
                            _listPending[pendingMsg.MainRequestKey] = storage;
                        }
                        bool isFull = storage.Append(pendingMsg.RawMessage, pendingMsg.Index, pendingMsg.Count);
                        if (isFull)
                        {
                            var fullRawStr = storage.GetFullMessge();
                            IMessage rawFullObj = JsonUtils.DeserializeMessage(fullRawStr);
                            if (Endpoint.IsWriteLog && rawFullObj != null)
                            {
                                #region Xu ly luu thong tin log msg
                                try
                                {
                                    var msgNameBiz = rawFullObj.GetType().ToString();
                                    decimal sizeBiz = (decimal)System.Text.ASCIIEncoding.UTF8.GetByteCount(fullRawStr) / (1024 * 1024);
                                    if (!Endpoint.DicMessageCount.ContainsKey(msgNameBiz))
                                    {
                                        Endpoint.DicMessageCount[msgNameBiz] = 0;
                                        Endpoint.DicMessageSize[msgNameBiz] = 0;
                                    }
                                    Endpoint.DicMessageCount[msgNameBiz] += 1;
                                    Endpoint.DicMessageSize[msgNameBiz] += sizeBiz;

                                    if (!Endpoint.DicMessageCountFull.ContainsKey(msgNameBiz))
                                    {
                                        Endpoint.DicMessageCountFull[msgNameBiz] = 0;
                                        Endpoint.DicMessageSizeFull[msgNameBiz] = 0;
                                    }
                                    Endpoint.DicMessageCountFull[msgNameBiz] += 1;
                                    Endpoint.DicMessageSizeFull[msgNameBiz] += sizeBiz;

                                    // sau 5p se ghi log tong so message gui di
                                    var timeCheck = (DateTime.Now - Endpoint.LastCheckCountMsg).TotalSeconds;
                                    if (timeCheck > Endpoint.TimeWriteLogMsgReceived)
                                    {
                                        // sau 5 phut ghi log 1 lan
                                        // sau 5 moi ghi log
                                        decimal countAll = 0;
                                        decimal countSizeAll = 0;
                                        decimal count5MinAll = 0;
                                        decimal count5MinSizeAll = 0;
                                        StringBuilder strDetail = new StringBuilder();
                                        var dicSort = Endpoint.DicMessageSizeFull.OrderBy(s => s.Key);
                                        foreach (var keyValue in dicSort)
                                        {
                                            string msgName = keyValue.Key;
                                            decimal sizeForMsg = keyValue.Value;
                                            decimal countForMsg = Endpoint.DicMessageCountFull[msgName];
                                            decimal count5Min = 0;
                                            if (Endpoint.DicMessageCount.ContainsKey(msgName))
                                                count5Min = Endpoint.DicMessageCount[msgName];

                                            decimal byte5Min = 0;
                                            if (Endpoint.DicMessageSize.ContainsKey(msgName))
                                                byte5Min = Endpoint.DicMessageSize[msgName];

                                            decimal avg5Min = count5Min / Endpoint.TimeWriteLogMsgReceived;
                                            countAll += countForMsg;
                                            countSizeAll += sizeForMsg;
                                            count5MinAll += count5Min;
                                            count5MinSizeAll += byte5Min;

                                            strDetail.AppendLine(GetLog(msgName, countForMsg, sizeForMsg, count5Min, byte5Min, avg5Min));
                                        }
                                        decimal avgAll5Min = count5MinAll / Endpoint.TimeWriteLogMsgReceived;
                                        var textLogFull = string.Format(
                                            "Total message = {0} with size = {1} MB; In 5min count = {2} and size = {3} MB; avg 5 min = {4} msg/s",
                                            countAll, countSizeAll.ToString("#,##0.0000"), count5MinAll, count5MinSizeAll.ToString("#,##0.0000"), avgAll5Min.ToString("#,##0.00"));
                                        StringBuilder strBuild = new StringBuilder();
                                        strBuild.AppendLine(string.Format("Thoi diem dang nhap {0}", Endpoint.StartTimeLogin));
                                        strBuild.AppendLine(textLogFull);
                                        strBuild.AppendLine("Thong tin chi tiet : ");
                                        strBuild.AppendLine(strDetail.ToString());
                                        LogTo.Info(strBuild.ToString());

                                        Endpoint.DicMessageCount = new Dictionary<string, decimal>();
                                        Endpoint.DicMessageSize = new Dictionary<string, decimal>();
                                        Endpoint.LastCheckCountMsg = DateTime.Now;
                                    }
                                }
                                catch (Exception ex1)
                                {
                                    LogTo.Error(ex1.ToString());
                                }
                                #endregion
                            }
                            messageEvent.Message = rawFullObj;
                            storage.Dispose();
                            _listPending.Remove(pendingMsg.MainRequestKey);
                        }
                    }
                    catch (Exception ex)
                    {
                        //neu phat sinh ra loi thi ghi ra console va thuc hien close network de load lai du lieu.
                        LogTo.Error(ex.ToString());
                        //neu khong hop le thi canh bao
                        _processor.ProcessControlerMsg(new InvalidMessageSeqEvent(), messageEvent.QueueType);
                    }
                }
                else if (!messageEvent.IsObsolate)
                {
                    messageEvent.Message = bizData;
                }
            }
            data.SetMessage(messageEvent);
        }

        private string GetLog(string msgName, decimal countForMsg, decimal sizeForMsg,
            decimal count5Min, decimal byte5Min, decimal avg5Min)
        {
            //50 ky tu dau tien danh cho ten msg
            msgName = msgName.PadRight(70);
            var intNum = 11;
            string countForMsgStr = countForMsg.ToString("#,##0").PadRight(intNum);
            string sizeForMsgStr = sizeForMsg.ToString("#,##0.000").PadRight(intNum);
            string count5MinStr = count5Min.ToString("#,##0").PadRight(intNum);
            string byte5MinStr = byte5Min.ToString("#,##0.000").PadRight(intNum);
            string avg5MinStr = avg5Min.ToString("#,##0.000").PadRight(intNum);

            var text = string.Format(
                "- {0} - total msg = {1} with size = {2} MB; ---  In 5 minute count = {3} with size = {4} MB; --- avg 5 min = {5} msg/s",
                msgName, countForMsgStr, sizeForMsgStr, count5MinStr, byte5MinStr, avg5MinStr);
            return text;
        }
    }
}
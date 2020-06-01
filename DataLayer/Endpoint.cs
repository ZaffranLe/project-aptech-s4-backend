using System;
using System.Collections.Generic;
using Anotar.NLog;
using QuantEdge.Common.Enum;
using QuantEdge.Lib.Common;
using QuantEdge.Lib.Disruptor;
using QuantEdge.Lib.Interface;
using QuantEdge.Message.Common;

namespace QuantEdge.Lib
{
    public sealed class Endpoint : IDisposable
    {
        private NetworkClient _endpoint;
        private IReceiveDisruptor _receiveDisruptor;
        private IPublishDisruptor _publishDisruptor;
        private IProcess _processor;
        #region Chi dung luu log message
        public static DateTime StartTimeLogin = DateTime.Now; //thoi gian login phan mem
        public static DateTime LastCheckCountMsg = DateTime.Now; //Thoi gian check msg lan cuoi
        public static int TimeWriteLogMsgReceived = 60 * 5; //5 min
        public static bool IsWriteLog = false; //5 min
        public static bool IsWriteLogInfo = false; //Ghi lai toan bo thong tin msg 
        public static Dictionary<string, decimal> DicMessageSizeFull = new Dictionary<string, decimal>(); //Luu thong tin tong so msg hien tai
        public static Dictionary<string, decimal> DicMessageCountFull = new Dictionary<string, decimal>(); //Luu thong tin tong so msg hien tai
        public static Dictionary<string, decimal> DicMessageSize = new Dictionary<string, decimal>(); //Luu thong tin tong so msg trong 5 phut
        public static Dictionary<string, decimal> DicMessageCount = new Dictionary<string, decimal>(); //Luu thong tin tong so msg trong 5 phut
        #endregion

        public Endpoint(IProcess processor, int maxSize, int inBufferSize, int outBufferSize, bool isWorker = false, bool isBStarMode = false, WorkerType? workerType = null, bool enableLoader = false)
        {
            _processor = processor;
            //ghi nhan hoat dong cua disruptor
            _receiveDisruptor = new ReceiveDisruptor(_processor, inBufferSize, isWorker, isBStarMode, workerType);
            //tao rieng network cho tung loai endpoint
            _endpoint = new NetworkClient(_receiveDisruptor);
            DataLoader loader = null;
            if (enableLoader)
                loader = new DataLoader(_processor);
            _publishDisruptor = new PublishDisruptor(_endpoint, maxSize, outBufferSize, loader);
        }

        public bool IsMaster { get { return _endpoint.IsMaster; } }

        public void Dispose()
        {
            try
            {
                if (_receiveDisruptor != null)
                    _receiveDisruptor.Dispose();
                if (_publishDisruptor != null)
                    _publishDisruptor.Dispose();
                if (_endpoint != null)
                    _endpoint.Dispose();
            }
            catch (Exception ex)
            {
                LogTo.Error(ex.ToString());
            }
            finally
            {
                _receiveDisruptor = null;
                _publishDisruptor = null;
                _endpoint = null;
            }
        }

        public bool SendBizMessage(IMessage message, byte flag = (byte)QueueType.MainQueue)
        {
            try
            {
                if (message == null)
                    throw new Exception("null message");
                if (_endpoint == null)
                    throw new Exception("null endpoint");

                return _publishDisruptor.Publish(new List<IMsg>() { message }, flag);
            }
            catch (Exception ex)
            {
                LogTo.Error(ex.ToString());
            }
            return false;
        }

        public bool SendControlerMessage(IControler message, byte flag = (byte)QueueType.MainQueue)
        {
            try
            {
                if (message == null)
                    throw new Exception("null message");
                if (_publishDisruptor != null)
                    return _publishDisruptor.Publish(new List<IMsg>() { message }, flag);
            }
            catch (Exception ex)
            {
                LogTo.Error(ex.ToString());
            }
            return false;
        }

        public bool SendMessage(List<IMsg> output, byte queueType)
        {
            try
            {
                if (output == null)
                    throw new Exception("null message");
                if (_publishDisruptor != null)
                    return _publishDisruptor.Publish(output, queueType);
            }
            catch (Exception ex)
            {
                LogTo.Error(ex.ToString());
            }
            return false;
        }
    }
}
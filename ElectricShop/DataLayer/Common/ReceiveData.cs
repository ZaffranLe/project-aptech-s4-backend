using System;
using System.Threading;
using QuantEdge.Lib.Interface;
using QuantEdge.Message.Common;
using Anotar.NLog;
namespace QuantEdge.Lib.Common
{
    using System.Collections.Generic;
    using System.Diagnostics;

    using QuantEdge.Common;

    public class ReceiveData : IDisposable, ICloneable, ISequencerEntry
    {
        private static Dictionary<ulong, LogItem> _dicDeliveryTag = new Dictionary<ulong, LogItem>();

        private const int OVER_BUFFER_SIZE = 1024 * 1024;
        public ulong DeliveryTag { get; set; }
        public byte[] RawMsg { get; set; }
        public IMsg Message { get; set; }
        public byte QueueType { get; set; }
        public bool IsObsolate { get; set; }

        public void Dispose()
        {
            var requireGC = (RawMsg != null && RawMsg.Length > OVER_BUFFER_SIZE);
            if (requireGC)
            {
                var a = Message;
                if (a != null)
                {
                    LogTo.Error("MSG_OVERSITE_RECEIVEDATA: " + a);
                }
            }
            if (Message != null)
                Message.Dispose();
            Message = null;
            RawMsg = null;
            if (requireGC)
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public void SetMessage(object bizMsg)
        {
            if (bizMsg is ReceiveData)
            {
                var obj = bizMsg as ReceiveData;
                DeliveryTag = obj.DeliveryTag;
                RawMsg = obj.RawMsg;
                Message = obj.Message;
                QueueType = obj.QueueType;
                IsObsolate = obj.IsObsolate;
            }
        }

        public void LogReceive()
        {
            try
            {
                if (!TestLog.DevelopmentLog) return;
                if (DeliveryTag <= 0 || RawMsg == null) return;
                var item = new LogItem();
                item.LogReceive(DeliveryTag, RawMsg.Length);
                lock (_dicDeliveryTag)
                {
                    _dicDeliveryTag[DeliveryTag] = item;
                }
            }
            catch (Exception ex)
            {
                LogTo.Error(ex.ToString() + " IS DELIVERY TAG = " + DeliveryTag);
            }
        }

        public void LogPreProcess()
        {
            try
            {
                if (!TestLog.DevelopmentLog) return;
                if (DeliveryTag <= 0) return;
                lock (_dicDeliveryTag)
                {
                    if (_dicDeliveryTag.ContainsKey(DeliveryTag))
                    {
                        var item = _dicDeliveryTag[DeliveryTag];
                        item.LogPreProcess(DeliveryTag, Message.GetType().Name);
                    }
                }
            }
            catch (Exception ex)
            {
                LogTo.Error(ex.ToString() + " IS DELIVERY TAG = " + DeliveryTag);
            }
        }

        public void LogProcess()
        {
            try
            {
                if (!TestLog.DevelopmentLog) return;
                if (DeliveryTag <= 0) return;
                lock (_dicDeliveryTag)
                {
                    if (_dicDeliveryTag.ContainsKey(DeliveryTag))
                    {
                        var item = _dicDeliveryTag[DeliveryTag];
                        item.LogProcess(DeliveryTag);
                    }
                }
            }
            catch (Exception ex)
            {
                LogTo.Error(ex.ToString() + " IS DELIVERY TAG = " + DeliveryTag);
            }
        }

        public static void LogPublish(ulong tag, int responseCount, long responseSize)
        {
            try
            {
                if (!TestLog.DevelopmentLog) return;
                //timer dung khi gui msg di
                //count se khong bao gom msg ack va controlers
                //kich thuoc cua cac msg khong bao gom msg ack va controlers
                if (tag <= 0) return;
                lock (_dicDeliveryTag)
                {
                    if (_dicDeliveryTag.ContainsKey(tag))
                    {
                        var item = _dicDeliveryTag[tag];
                        item.LogPublish(tag, responseCount, responseSize);
                        // remove item
                        _dicDeliveryTag.Remove(tag);
                    }
                }
            }
            catch (Exception ex)
            {
                LogTo.Error(ex.ToString() + " IS DELIVERY TAG = " + tag);
            }
        }
    }

    public class LogItem
    {
        //[RUN][_msgName][_timeProcess][_inputSize][_timePublish][_responseCount][ResponseSize]
        private const string Format = "[RUN][{0}][{1}][{2}][{3}][{4}][{5}]";
        private Stopwatch _timer;
        private string _msgName;
        private ulong _tag;
        private long _inputSize;
        private double _timeProcess = 0;
        private double _timePublish = 0;

        public void LogReceive(ulong tag, long size)
        {
            try
            {
                this._tag = tag;
                this._inputSize = size;
                this._timer = Stopwatch.StartNew();
            }
            catch (Exception ex)
            {
                LogTo.Error(ex.ToString());
            }
        }

        public void LogProcess(ulong tag)
        {
            try
            {
                if (this._tag != tag) return;
                this._timer.Stop();
                this._timeProcess = _timePublish + this._timer.Elapsed.TotalMilliseconds;
                this._timer.Restart();
            }
            catch (Exception ex)
            {
                LogTo.Error(ex.ToString());
            }
        }

        public void LogPreProcess(ulong tag, string name)
        {
            try
            {
                if (this._tag != tag) return;
                this._msgName = name;
            }
            catch (Exception ex)
            {
                LogTo.Error(ex.ToString());
            }
        }

        public void LogPublish(ulong tag, int responseCount, long responseSize)
        {
            try
            {
                if (this._tag != tag) return;
                this._timer.Stop();
                this._timePublish = this._timer.Elapsed.TotalMilliseconds;

                TestLog.WriteLine(Format, this._msgName, this._timeProcess, this._inputSize, this._timePublish, responseCount, responseSize);
            }
            catch (Exception ex)
            {
                LogTo.Error(ex.ToString());
            }
        }
    }
}
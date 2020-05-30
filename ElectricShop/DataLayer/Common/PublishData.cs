using System;
using System.Collections.Generic;
using QuantEdge.Lib.Interface;
using QuantEdge.Message.Common;
using Anotar.NLog;
namespace QuantEdge.Lib.Common
{
    public sealed class PackageData : IDisposable, ICloneable, ISequencerEntry
    {
        public IMsg Message { get; set; }
        public List<byte[]> BytesMsg { get; set; }
        public byte QueueType { get; set; }
        public bool IsSendToQueue { get; set; }

        public void Dispose()
        {
            if (Message != null)
                Message.Dispose();
            Message = null;
            if (BytesMsg != null)
            {
                BytesMsg.Clear();
                BytesMsg = null;
            }
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public void SetMessage(object bizMsg)
        {
            if (bizMsg is PackageData)
            {
                var obj = bizMsg as PackageData;
                Message = obj.Message;
                BytesMsg = obj.BytesMsg;
                QueueType = obj.QueueType;
                IsSendToQueue = obj.IsSendToQueue;
            }
        }
    }

    public class PublishData : IDisposable, ISequencerEntry
    {
        private const int OVER_BUFFER_SIZE = 1024 * 1024;
        public List<PackageData> ListOutput { get; set; }

        public void Dispose()
        {
            var requireGC = false;
            if (ListOutput != null)
            {
                foreach (var publishMessage in ListOutput)
                {
                    if (publishMessage.BytesMsg != null && publishMessage.BytesMsg[0].Length > OVER_BUFFER_SIZE)
                    {
                        var a = publishMessage.Message;
                        if (a != null)
                        {
                            LogTo.Error("MSG_OVERSITE_PUBLISHDATA: " + a);
                        }
                        requireGC = true;
                    }
                    publishMessage.Dispose();
                }
                ListOutput.Clear();
            }

            ListOutput = null;
            if (requireGC)
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        public void SetMessage(object bizMsg)
        {
            if (bizMsg is PublishData)
            {
                var obj = bizMsg as PublishData;
                ListOutput = obj.ListOutput;
            }
        }
    }
}
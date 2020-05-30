using System;
using System.Threading;
using Anotar.NLog;
using Disruptor;
using QuantEdge.Common;
using QuantEdge.Lib.Common;
using QuantEdge.Lib.Interface;
using QuantEdge.Message.Common;
using QuantEdge.Message.Request;
using QuantEdge.Message.Response;

namespace QuantEdge.Lib.Sequencer
{
    using System.Collections.Generic;
    using System.Linq;

    using QuantEdge.Message.Controler;

    internal sealed class EventPublisher : IEventHandler<PublishData>
    {
        private readonly IEndpoint _endpoint;

        internal EventPublisher(IEndpoint endpoint)
        {
            _endpoint = endpoint;
        }

        public void OnEvent(PublishData data, long sequence, bool endOfBatch)
        {
            var messageEvent = data;
            if (messageEvent.ListOutput == null) return;
            foreach (var publishMessage in messageEvent.ListOutput)
            {
                if (publishMessage.Message == null) continue;

                var bizMsg = publishMessage.Message.Clone() as IMsg;

                if (bizMsg is IControler)
                    Send(bizMsg);
                else if (bizMsg is IMessage)
                {
                    var msg = bizMsg as IMessage;
                    if (publishMessage.BytesMsg != null && publishMessage.BytesMsg.Count > 0)
                    {
                        Send(new SendingMessage()
                        {
                            IsSendToQueue = msg.IsSendToQueue,
                            Data = publishMessage.BytesMsg,
                            Topic = msg.SendingTopic,
                            Ttl = 0,
                            RequireSend = msg is GetStartupMemoryRequest
                        });
                    }
                }
            }
            LogOutput(messageEvent.ListOutput);
            data.Dispose();
            data = null;
        }

        private void Send(IMsg msg)
        {
            while (!_endpoint.Send(msg))
            {
                Thread.Sleep(TimeSpan.FromSeconds(3));//3 second sau thi chay lai
                LogTo.Error("loopback");
            }
        }

        private void LogOutput(List<PackageData> list)
        {
            if (!TestLog.DevelopmentLog) return; //Su dung test log moi vao ham nay
            ulong tag = 0;
            int count = 0;
            long size = 0;
            long totalRecord = 0;
            string msgName = "";
            foreach (var publishMessage in list)
            {
                if (publishMessage.Message == null) continue;
                if (publishMessage.Message is IControler)
                {
                    if (publishMessage.Message is JournalMessage)
                    {
                        tag = (publishMessage.Message as JournalMessage).Tag;
                    }
                    continue;
                };
                if (publishMessage.Message is IMessage)
                {
                    if (publishMessage.BytesMsg != null && publishMessage.BytesMsg.Count > 0)
                    {
                        count++;
                        size += publishMessage.BytesMsg.Sum(s => s.Length);
                        var msg = publishMessage.Message as IMessage;
                        totalRecord = msg.TotalRecord;
                        msgName = msg.GetType().Name;
                    }
                }
            }

            if (tag > 0)
            {
                ReceiveData.LogPublish(tag, count, size);
            }

            if (totalRecord > 0 && !string.IsNullOrEmpty(msgName))
            {
                LogTo.Info("Message {0} is {1} record and {2} byte", msgName, totalRecord, size);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Disruptor;
using QuantEdge.Lib.Common;
using QuantEdge.Lib.Interface;
using QuantEdge.Lib.Utils;
using QuantEdge.Message.Common;
using QuantEdge.Message.Controler;
using QuantEdge.Message.Request;
using QuantEdge.Message.Response;

namespace QuantEdge.Lib.Sequencer
{
    internal sealed class EventMarshaler : IEventHandler<PublishData>
    {
        private readonly int _maxSize;
        private readonly ILoader _loader;

        public EventMarshaler(int maxSize, ILoader loader)
        {
            _maxSize = maxSize;
            if (_maxSize < 255)
                _maxSize = 255;//set toi thieu cung phai 255 chu
            _loader = loader;
        }

        public void OnEvent(PublishData data, long sequence, bool endOfBatch)
        {
            var messageEvent = data;
            if (messageEvent == null || messageEvent.ListOutput == null) return;
            foreach (var publishMsg in messageEvent.ListOutput)
            {
                if (publishMsg.Message is IControler)
                {
                    //neu la connect message
                    if (publishMsg.Message is ConnectMessage)
                    {
                        var connectMsg = publishMsg.Message as ConnectMessage;
                        if (connectMsg.StartupMessage != null)
                        {
                            //thuc hien Marshal messase check AutoUpdate
                            var str = JsonUtils.SerializeMessage(connectMsg.StartupMessage);
                            connectMsg.BytesMsg = EncodingUtils.GetBytes(str);
                            publishMsg.Message = connectMsg;
                        }
                    }
                }
                else if (publishMsg.Message is IMessage)
                {
                    if (publishMsg.Message is GetStartupMemoryRequest && _loader != null)
                    {
                        var req = publishMsg.Message as GetStartupMemoryRequest;
                        _loader.ProcessData(ref req);
                    }
                    else
                    {
                        var message = publishMsg.Message as IMessage;
                        publishMsg.IsSendToQueue = message.IsSendToQueue;
                        var jsonStr = JsonUtils.SerializeMessage(message);

                        if (message is Response && jsonStr.Length > _maxSize)
                        {
                            //chi thuc hien chia nho khi msg la response va kich thuoc lon hon kich thuc cho phep
                            List<string> arr = Split(jsonStr, _maxSize);
                            int count = arr.Count;
                            publishMsg.BytesMsg = new List<byte[]>();
                            for (int i = 0; i < count; i++)
                            {
                                var msg = new PartialMessage()
                                {
                                    Count = count,
                                    Index = i,
                                    MainRequestKey = (message as Response).RequestKey,
                                    RawMessage = arr[i],
                                    SendingTopic = message.SendingTopic,
                                    MainMsgType = message.GetType().ToString(),
                                    RequestKey = "",
                                };
                                var str = JsonUtils.Serialize(msg);
                                byte[] byteMsg = EncodingUtils.GetBytes(str);
                                if (byteMsg == null)
                                    throw new Exception("not parse PartialMessage");
                                publishMsg.BytesMsg.Add(byteMsg);
                            }
                        }
                        else publishMsg.BytesMsg = new List<byte[]>() { EncodingUtils.GetBytes(jsonStr) };
                    }

                }
            }
            data.SetMessage(messageEvent);
        }

        private static List<string> Split(string str, int chunkSize)
        {
            int length = str.Length;
            int index = length / chunkSize;
            if (index * chunkSize < length)
                index++;
            return
                new List<string>(
                    Enumerable.Range(0, index).Select(
                        i =>
                        str.Substring(i * chunkSize, (i + 1) == index ? (length - i * chunkSize) : chunkSize)));
        }
    }
}
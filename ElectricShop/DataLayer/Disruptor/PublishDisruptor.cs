using System;
using System.Collections.Generic;
using Anotar.NLog;
using Disruptor;
using QuantEdge.Lib.Common;
using QuantEdge.Lib.Interface;
using QuantEdge.Lib.Sequencer;
using QuantEdge.Message.Common;

namespace QuantEdge.Lib.Disruptor
{
    public sealed class PublishDisruptor : IPublishDisruptor
    {
        private DataHandlingSequencer<PublishData> _disruptor;

        public PublishDisruptor(IEndpoint endpoint, int maxSize, int bufferSize, ILoader loader)
        {
            var list = new IEventHandler<PublishData>[]
                    {
                        new EventMarshaler(maxSize, loader),
                        new EventPublisher(endpoint),
                    };
            _disruptor = new DataHandlingSequencer<PublishData>(SequencerFactoryEntry.NewPublishData, bufferSize);
            _disruptor.SetFunctions(list).HandleExeceptionWith(new ExceptionHandler<PublishData>());
            //ghi nhan du lieu
            _disruptor.Start();
        }

        public bool Publish(List<IMsg> output, byte queueType)
        {
            try
            {
                if (_disruptor == null)
                    return false;

                var messageEvent = new PublishData() { ListOutput = new List<PackageData>() };

                //for list du lieu dong goi thanh list publish message
                foreach (var imsg in output)
                {
                    var message = new PackageData
                    {
                        Message = imsg,
                        QueueType = queueType,
                    };
                    messageEvent.ListOutput.Add(message);
                }
                return _disruptor.Publish(messageEvent);
            }
            catch (Exception ex)
            {
                LogTo.Error(ex.ToString());
            }
            return false;
        }

        public void Dispose()
        {
            try
            {
                if (_disruptor != null)
                    _disruptor.Dispose();
            }
            catch (Exception ex)
            {
                LogTo.Error(ex.ToString());
            }
            finally
            {
                _disruptor = null;
            }
        }
    }
}
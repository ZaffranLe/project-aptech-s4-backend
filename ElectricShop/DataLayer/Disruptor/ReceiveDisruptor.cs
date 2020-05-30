using Disruptor;
using QuantEdge.Common.Enum;
using QuantEdge.Lib.Common;
using QuantEdge.Lib.Interface;
using QuantEdge.Lib.Sequencer;
using QuantEdge.Message.Common;

namespace QuantEdge.Lib.Disruptor
{
    public sealed class ReceiveDisruptor : IReceiveDisruptor
    {
        private DataHandlingSequencer<ReceiveData> _disruptor;
        public ReceiveDisruptor(IProcess processor, int bufferSize, bool isWorker = false, bool isBStarMode = false, WorkerType? workerType = null)
        {
            _disruptor = new DataHandlingSequencer<ReceiveData>(SequencerFactoryEntry.NewReceiveData, bufferSize);
            var listTasks = new IEventHandler<ReceiveData>[]
                    {
                        new EventUnMarshaler(processor, isWorker, isBStarMode),
                        new EventProcessor(processor)
                    };
            _disruptor.SetFunctions(listTasks).HandleExeceptionWith(new ExceptionHandler<ReceiveData>());

            //ghi nhan du lieu chay
            _disruptor.Start();
        }

        public bool Receive(byte[] byteData, byte queueType, ulong tag)
        {
            if (_disruptor == null)
                return false;

            var rawData = new ReceiveData()
                {
                    QueueType = queueType,
                    RawMsg = byteData,
                    DeliveryTag = tag
                };
            rawData.LogReceive();
            return _disruptor.Publish(rawData);
        }

        public bool Receive(IControler message)
        {
            if (_disruptor == null)
                return false;

            var rawData = new ReceiveData
            {
                Message = message,
            };
            return _disruptor.Publish(rawData);
        }

        public void Dispose()
        {
            if (_disruptor != null)
            {
                _disruptor.Dispose();
                _disruptor = null;
            }
        }
    }
}
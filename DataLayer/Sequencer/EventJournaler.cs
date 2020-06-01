using System.Text;
using Disruptor;
using QuantEdge.Lib.Common;
using QuantEdge.Lib.Interface;
using QuantEdge.Lib.Utils;
using QuantEdge.Message.Broadcast.Sinker;
using QuantEdge.Message.Common;
using QuantEdge.Message.Controler;

namespace QuantEdge.Lib.Sequencer
{
    internal sealed class EventJournaler : IEventHandler<ISequencerEntry<ReceiveData>>
    {
        private readonly IProcess _messageBus;

        internal EventJournaler(IProcess messageBus)
        {
            _messageBus = messageBus;
        }

        void IEventHandler<ISequencerEntry<ReceiveData>>.OnNext(
            ISequencerEntry<ReceiveData> data, long sequence, bool endOfBatch)
        {
            if (_messageBus != null)
            {
                var receivedData = data.GetMessage();
                if (receivedData != null)
                {
                    //neu la control message thi khong can Journal
                    if (receivedData.Message is IControler || receivedData.RawMsg == null)
                        return;

                    //neu la control message thi khong can Journal
                    if (!(receivedData.Message is IMessage))
                        return;

                    //thuc hien send Ack message
                    var journal = new JournalMessage()
                    {
                        Tag = receivedData.DeliveryTag,
                        IsAck = true,
                    };
                    switch (receivedData.QueueType)
                    {
                        case (byte)QueueType.MainQueue:
                            //Check co can gui replicate message hay ko
                            if (_messageBus.RequireReplication)
                                journal.ReplicateMsg = receivedData.RawMsg;
                            //Check co can gui save sinker message hay ko
                            if (_messageBus.IsWorkerBStar)
                            {
                                if (receivedData.Message is IMessage)
                                {
                                    var bizMsg = receivedData.Message as IMessage;
                                    if (!bizMsg.IsNotPersist)
                                    {
                                        journal.SinkMsg = CreateSinkerMsg(receivedData.RawMsg);
                                    }
                                }
                            }
                            _messageBus.Send(journal, (byte)QueueType.MainQueue);
                            break;
                        case (byte)QueueType.ReplicatorQueue:
                            _messageBus.Send(journal, (byte)QueueType.ReplicatorQueue);
                            break;
                    }
                }
            }
        }

        private byte[] CreateSinkerMsg(byte[] msg)
        {
            var reqSinker = new StoreMessageBroadcast()
            {
                FromWorker = _messageBus.WorkerType,
                RawMessageToSave = Encoding.UTF8.GetString(msg)
            };
            var jsonString = JsonUtils.Serialize(reqSinker);
            return Encoding.UTF8.GetBytes(jsonString);
        }
    }
}
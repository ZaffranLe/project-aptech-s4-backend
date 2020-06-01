using QuantEdge.Lib.Common;

namespace QuantEdge.Lib.Interface
{
    internal interface ISequencerEntry
    {
        void SetMessage(object bizMsg);
    }

    internal class SequencerFactoryEntry
    {
        public static ReceiveData NewReceiveData()
        {
            return new ReceiveData();
        }
        public static PublishData NewPublishData()
        {
            return new PublishData();
        }
    }
}
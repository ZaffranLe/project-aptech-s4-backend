using Disruptor;
using QuantEdge.Lib.Common;
using QuantEdge.Lib.Interface;

namespace QuantEdge.Lib.Sequencer
{
    internal sealed class EventProcessor : IEventHandler<ReceiveData>
    {
        private readonly IProcess _processor;

        internal EventProcessor(IProcess processor)
        {
            _processor = processor;
        }

        public void OnEvent(ReceiveData data, long sequence, bool endOfBatch)
        {
            var messageEvent = data;
            if (messageEvent == null || messageEvent.Message == null)
                return;
            //neu message IsObsolate thi bo qua
            if (messageEvent.IsObsolate) return;
            //thuc hien goi BizLayer xu ly
            data.LogPreProcess();
            _processor.ProcessMsg(messageEvent, messageEvent.QueueType);
            //giai phong tai nguyen
            data.LogProcess();
            data.Dispose();
            data = null;
         }
    }
}
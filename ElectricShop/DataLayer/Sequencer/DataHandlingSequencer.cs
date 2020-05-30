using System;
using System.Threading.Tasks;
using Anotar.NLog;
using Disruptor;
using Disruptor.Dsl;
using QuantEdge.Lib.Interface;

namespace QuantEdge.Lib.Sequencer
{
    internal class DataHandlingSequencer<T> : IDisposable where T : class
    {
        private Disruptor<T> _disruptor;
        private IExceptionHandler<T> _exceptionHandler;
        private RingBuffer<T> _ringBuffer;
        private IEventHandler<T>[] _listTasks;

        public DataHandlingSequencer(Func<T> sequencerEntryFactory, int bufferSize)
        {
            _disruptor = new Disruptor<T>(sequencerEntryFactory, bufferSize,
                TaskScheduler.Default, ProducerType.Single, new BlockingWaitStrategy());
        }

        public DataHandlingSequencer<T> SetFunctions(IEventHandler<T>[] list)
        {
            _listTasks = list;
            return this;
        }

        public DataHandlingSequencer<T> HandleExeceptionWith(IExceptionHandler<T> handler)
        {
            _exceptionHandler = handler;
            return this;
        }

        /// <summary>
        ///     Bootstraps the <see cref="Disruptor" /> and starts it.
        /// </summary>
        public void Start()
        {
            if (_exceptionHandler == null)
                throw new ArgumentNullException("Exception EventHandler cannot be null");

            _disruptor.SetDefaultExceptionHandler(_exceptionHandler);

            if (_listTasks == null || _listTasks.Length <= 0)
            {
                throw new ArgumentNullException("list task still null");
            }

            EventHandlerGroup<T> group = null;
            foreach (var eventHandler in _listTasks)
            {
                if (group == null)
                    group = _disruptor.HandleEventsWith(eventHandler);
                else
                    group = group.Then(eventHandler);
            }

            _ringBuffer = _disruptor.Start();
        }

        public bool Publish(T rawData)
        {
            long sequence = _ringBuffer.Next();
            var storeEntry = _ringBuffer[sequence];
            var entry = storeEntry as ISequencerEntry;
            if (entry != null)
                entry.SetMessage(rawData);
            _ringBuffer.Publish(sequence);
            return true;
        }

        public void Dispose()
        {
            try
            {
                if (_disruptor != null)
                    _disruptor.Shutdown(new TimeSpan(10));
            }
            catch (Exception ex)
            {
                LogTo.Error(ex.ToString());
            }
            finally
            {
                _disruptor = null;
                _ringBuffer = null;
            }
        }

    }
}
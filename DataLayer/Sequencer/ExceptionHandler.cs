using System;
using Anotar.NLog;
using Disruptor;

namespace QuantEdge.Lib.Sequencer
{
    /// <summary>
    ///     Common Exception handler to handle any exeception thrown from any <see cref="IEventHandler{T}" />.
    /// </summary>
    internal class ExceptionHandler<T> : IExceptionHandler<T> where T : class
    {
        public void HandleEventException(Exception ex, long sequence, T eventToUse)
        {
            LogTo.Error("Following execption occured when processing event #{0} of type {1}:\n\t{2}",
               sequence, eventToUse, ex);
        }

        public void HandleOnStartException(Exception ex)
        {
            LogTo.Error("Following execption occured On Handler StartUp\n\t{0}", ex);
        }

        public void HandleOnShutdownException(Exception ex)
        {
            LogTo.Error("Following execption occured On Handler StartUp\n\t{0}", ex);
        }
    }
}
using QuantEdge.Lib.Common;
using QuantEdge.Message.Common;

namespace QuantEdge.Lib.Interface
{
    public interface IProcess
    {
        void ProcessMsg(ReceiveData msg, byte flag);
        void ProcessControlerMsg(IControler msg, byte flag);
        bool Send(IControler journalMsg, byte flag);
    }
}

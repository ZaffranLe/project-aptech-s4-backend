using QuantEdge.Message.Request;

namespace QuantEdge.Lib.Interface
{
    public interface ILoader
    {
        void ProcessData(ref GetStartupMemoryRequest request);
    }
}

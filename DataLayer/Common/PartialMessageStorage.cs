using System;
using System.Text;

namespace QuantEdge.Lib.Common
{
    public class PartialMessageStorage : IDisposable
    {
        private readonly int _count;
        private StringBuilder _builder;
        private int _currentIndex;

        public PartialMessageStorage(int count)
        {
            _count = count;
            _builder = new StringBuilder();
            _currentIndex = -1;
        }

        public void Dispose()
        {
            _builder = null;
        }

        public bool Append(string data, int index, int count)
        {
            if (_count != count)
                throw new Exception("Invalid message size");
            if (index < 0 || index >= _count)
                throw new Exception("Invalid message index");
            if (_currentIndex + 1 != index)
                throw new Exception("Invalid message index seq");
            _currentIndex = index;
            _builder.Append(data);

            return index == count - 1;
        }

        public string GetFullMessge()
        {
            return _builder.ToString();
        }
    }
}
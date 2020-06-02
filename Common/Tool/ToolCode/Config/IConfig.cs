using System;

namespace CommonicationMemory.Config
{
    public interface IConfig : IDisposable
    {
        string GetFileName();
    }
}

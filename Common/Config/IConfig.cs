using System;

namespace ElectricShop.Common.Config
{
    public interface IConfig : IDisposable
    {
        string GetFileName();
    }
}

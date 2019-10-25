using System;

namespace TrainsInfo.Common.Interfaces
{
    public interface IListener : IDisposable
    {
        IDataStream Accept();
    }
}

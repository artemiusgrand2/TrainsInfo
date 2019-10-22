using System;

namespace TrainsInfo.Common.Interfaces
{
    public interface IDataStream : IDisposable
    {
        bool Read(out object data);
    }
}

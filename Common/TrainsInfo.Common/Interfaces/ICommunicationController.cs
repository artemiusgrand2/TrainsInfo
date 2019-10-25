using System;

namespace TrainsInfo.Common.Interfaces
{
    public delegate void ErrorHandler<TSender, TValue>(TSender sender, TValue value);
    public interface ICommunicationController : IDisposable
    {
        event ErrorHandler<ICommunicationController, Exception> OnError;

        IDataStream Client { get; }

        void Start();
        void Stop();
    }
}

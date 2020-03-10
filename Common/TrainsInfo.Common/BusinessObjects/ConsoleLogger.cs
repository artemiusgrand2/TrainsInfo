using System;
using System.Threading;
using TrainsInfo.Common.Interfaces;

namespace TrainsInfo.Common.BusinessObjects
{
    public class ConsoleLogger : ILogger
    {
        public void ObligatoryInfo(string format, params object[] args)
        {
            Console.WriteLine("{0} [INFO][{1}] {2}", DateTime.Now, Thread.CurrentThread.ManagedThreadId,
                              string.Format(format, args));
        }

        public void LogInfo(string format, params object[] args)
        {
            Console.WriteLine("{0} [INFO][{1}] {2}", DateTime.Now, Thread.CurrentThread.ManagedThreadId,
                              string.Format(format, args));
        }

        public void LogWarn(string format, params object[] args)
        {
            Console.WriteLine("{0} [WARN][{1}] {2}", DateTime.Now, Thread.CurrentThread.ManagedThreadId,
                              string.Format(format, args));
        }

        public void LogError(string format, params object[] args)
        {
            Console.WriteLine("{0} [ERROR][{1}] {2}", DateTime.Now, Thread.CurrentThread.ManagedThreadId,
                              string.Format(format, args));
        }

        public void LogDebug(string format, params object[] args)
        {
            Console.WriteLine("{0} [DEBUG][{1}] {2}", DateTime.Now, Thread.CurrentThread.ManagedThreadId,
                              string.Format(format, args));
        }

        public void OthersTrainsInfo(string format, params object[] args) { }

        public void AreaSubTrainsInfo(string format, params object[] args) { }

        public void ClientsInfo(string data)
        {
        }
    }
}

using System;
using System.IO;
using System.Text;
using TrainsInfo.Common.Interfaces;
using log4net;

namespace TrainsInfo.Logger.Log4Net
{
    public class Log4NetLoggerWrapper : ILogger
    {
        private readonly ILog logger;

        public string PathClientInfo
        {
            get;
            internal set;
        }

        internal Log4NetLoggerWrapper(ILog log)
        {
            logger = log;
        }

        public void ObligatoryInfo(string format, params object[] args)
        {
            logger.InfoFormat(format, args);
        }

        public void LogInfo(string format, params object[] args)
        {
            logger.InfoFormat(format, args);
        }

        public void LogWarn(string format, params object[] args)
        {
            logger.WarnFormat(format, args);
        }

        public void LogError(string format, params object[] args)
        {
            logger.ErrorFormat(format, args);
        }

        public void LogDebug(string format, params object[] args)
        {
            logger.DebugFormat(format, args);
        }

    }
}

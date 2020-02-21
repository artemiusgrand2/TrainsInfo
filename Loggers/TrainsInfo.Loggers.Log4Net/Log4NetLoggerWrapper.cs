using System;
using System.IO;
using System.Text;
using TrainsInfo.Common.Interfaces;
using log4net;

namespace TrainsInfo.Logger.Log4Net
{
    public class Log4NetLoggerWrapper : ILogger
    {
        private readonly ILog commonLog;

        private readonly ILog othersTrains;

        public string PathClientInfo
        {
            get;
            internal set;
        }

        internal Log4NetLoggerWrapper(ILog commonLog, ILog othersTrains)
        {
            this.commonLog = commonLog;
            this.othersTrains = othersTrains;
        }

        public void ObligatoryInfo(string format, params object[] args)
        {
            commonLog.InfoFormat(format, args);
        }

        public void LogInfo(string format, params object[] args)
        {
            commonLog.InfoFormat(format, args);
        }

        public void LogWarn(string format, params object[] args)
        {
            commonLog.WarnFormat(format, args);
        }

        public void LogError(string format, params object[] args)
        {
            commonLog.ErrorFormat(format, args);
        }

        public void LogDebug(string format, params object[] args)
        {
            commonLog.DebugFormat(format, args);
        }

        public void OthersTrainsInfo(string format, params object[] args)
        {
            othersTrains.InfoFormat(format, args);
        }


    }
}

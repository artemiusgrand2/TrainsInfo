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

        private readonly ILog areaSubTrains;

        public string PathClientInfo
        {
            get;
            internal set;
        }

        internal Log4NetLoggerWrapper(ILog commonLog, ILog othersTrains, ILog areaSubTrains)
        {
            this.commonLog = commonLog;
            this.othersTrains = othersTrains;
            this.areaSubTrains = areaSubTrains;
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

        public void AreaSubTrainsInfo(string format, params object[] args)
        {
            areaSubTrains.InfoFormat(format, args);
        }


    }
}

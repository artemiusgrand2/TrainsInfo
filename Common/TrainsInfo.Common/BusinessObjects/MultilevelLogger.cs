using System;
using System.Collections.Generic;
using System.Text;
using TrainsInfo.Common.Enums;
using TrainsInfo.Common.Interfaces;
using TrainsInfo.Configuration.Records;

namespace TrainsInfo.Common.BusinessObjects
{
    public class MultilevelLogger : ILogger
    {
        private readonly ILogger loggerImplementation;
        private readonly LogLevel logLevel;
        private readonly bool isInfoEnabled;
        private readonly bool isInfoOthersTrains;
        private readonly bool isWarnEnabled;
        private readonly bool isErrorEnabled;
        private readonly bool isDebugEnabled;

        private MultilevelLogger(ILogger loggerImpl, LogLevel level)
        {
            loggerImplementation = loggerImpl;
            logLevel = level;
            if (((int)logLevel) > 1)
            {
                isInfoOthersTrains = isInfoEnabled = isWarnEnabled = isErrorEnabled = isDebugEnabled = true;
            }
            else if (((int)logLevel) > 0)
            {
                isInfoOthersTrains = isInfoEnabled = isErrorEnabled = isWarnEnabled = true;
                isDebugEnabled = false;
            }
            else
            {
                isErrorEnabled = true;
                isInfoOthersTrains = isWarnEnabled = isInfoEnabled = isDebugEnabled = false;
            }
        }

        public void ObligatoryInfo(string format, params object[] args)
        {
            loggerImplementation.LogInfo(format, args);
        }

        public void LogInfo(string format, params object[] args)
        {
            if (isInfoEnabled)
            {
                loggerImplementation.LogInfo(format, args);
            }
        }

        public void LogWarn(string format, params object[] args)
        {
            if (isWarnEnabled)
            {
                loggerImplementation.LogWarn(format, args);
            }
        }

        public void LogError(string format, params object[] args)
        {
            if (isErrorEnabled)
            {
                loggerImplementation.LogError(format, args);
            }
        }

        public void LogDebug(string format, params object[] args)
        {
            if (isDebugEnabled)
            {
                loggerImplementation.LogDebug(format, args);
            }
        }

        public void OthersTrainsInfo(string format, params object[] args)
        {
            if (isInfoOthersTrains)
            {
                loggerImplementation.OthersTrainsInfo(format, args);
            }
        }


        public static MultilevelLogger Create(LoggerRecord record, LogLevel logLevel)
        {
            ILogger loggerImpl = LoggerPluginWrapper.Instance[record.Type].Create(record);
            return Create(loggerImpl, logLevel);
        }

        public static MultilevelLogger Create(ILogger loggerImpl, LogLevel logLevel)
        {
            MultilevelLogger result = new MultilevelLogger(loggerImpl, logLevel);
            return result;
        }
    }
}

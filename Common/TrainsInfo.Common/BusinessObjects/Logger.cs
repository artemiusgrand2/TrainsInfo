using System;
using TrainsInfo.Configuration.Records;
using TrainsInfo.Common.Enums;
using TrainsInfo.Common.Interfaces;

namespace TrainsInfo.Common.BusinessObjects
{
    public class Logger
    {
        private static bool isInitialized = false;
        private static ILogger logger;
        private static readonly object locker = new object();

        public static ILogger Log
        {
            get
            {
                return logger;
            }
        }

        public static void Init(LoggerRecord record, LogLevel logLevel)
        {
            if (!isInitialized &&
                logger == null)
            {
                lock (locker)
                {
                    if (!isInitialized &&
                        logger == null)
                    {
                        if (record == null ||
                                 !LoggerPluginWrapper.Instance.IsLoadedType(record.Type))
                        {
                            Console.WriteLine("!!! Using Console Logger !!!");
                            logger = new ConsoleLogger();
                        }
                        else
                            logger = MultilevelLogger.Create(record, logLevel);
                        //
                        isInitialized = true;
                    }
                }
            }
        }
    }
}

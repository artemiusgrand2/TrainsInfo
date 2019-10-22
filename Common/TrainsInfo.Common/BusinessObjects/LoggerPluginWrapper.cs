using System;
using TrainsInfo.Common.Interfaces;

namespace TrainsInfo.Common.BusinessObjects
{
    public class LoggerPluginWrapper
    {
        private static readonly LoggerPluginWrapper instance;

        public static LoggerPluginWrapper Instance
        {
            get
            {
                return instance;
            }
        }

        static LoggerPluginWrapper()
        {
            instance = new LoggerPluginWrapper();
            instance.manager.Load(AppDomain.CurrentDomain.BaseDirectory, "TrainsInfo.Logger.*.dll");
        }

        private PluginManager<ILoggerFactory> manager;

        public ILoggerFactory this[string dataType]
        {
            get
            {
                return instance.manager.Plugins[dataType];
            }
        }

        private LoggerPluginWrapper()
        {
            manager = new PluginManager<ILoggerFactory>();
        }

        public bool IsLoadedType(string type)
        {
            return instance.manager.Plugins.ContainsKey(type);
        }
    }
}

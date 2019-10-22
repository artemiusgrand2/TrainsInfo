using System;
using System.Collections.Generic;
using TrainsInfo.Common.Interfaces;

namespace TrainsInfo.Common.BusinessObjects
{
    public class DataParserPluginWrapper
    {
        private static readonly DataParserPluginWrapper instance;

        public static DataParserPluginWrapper Instance
        {
            get
            {
                return instance;
            }
        }

        static DataParserPluginWrapper()
        {
            instance = new DataParserPluginWrapper();
            instance.manager.Load(AppDomain.CurrentDomain.BaseDirectory, "TrainsInfo.DataParser.*.dll");
        }

        private PluginManager<IDataParserFactory> manager;

        public IDataParserFactory this[string dataType]
        {
            get
            {
                if (instance.manager.Plugins.ContainsKey(dataType))
                    return instance.manager.Plugins[dataType];
                else
                    throw new Exception(string.Format("Not found .dll parser with type '{0}'", dataType));
            }
        }

        private DataParserPluginWrapper()
        {
            manager = new PluginManager<IDataParserFactory>();
        }
    }
}

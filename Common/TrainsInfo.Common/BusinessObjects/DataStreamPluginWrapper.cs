using System;
using TrainsInfo.Common.BusinessObjects;
using TrainsInfo.Common.Interfaces;

namespace TrainsInfo.Common.BusinessObjects
{
    public class DataStreamPluginWrapper
    {
        private static readonly DataStreamPluginWrapper instance;

        public static DataStreamPluginWrapper Instance
        {
            get
            {
                return instance;
            }
        }

        static DataStreamPluginWrapper()
        {
            instance = new DataStreamPluginWrapper();
            instance.manager.Load(AppDomain.CurrentDomain.BaseDirectory, "TrainsInfo.DataStreams.*.dll");
        }

        private readonly PluginManager<IDataStreamFactory> manager;

        public IDataStreamFactory this[string dataType]
        {
            get
            {
                if (instance.manager.Plugins.ContainsKey(dataType))
                    return instance.manager.Plugins[dataType];
                else
                    throw new Exception(string.Format("Not found .dll datastream with type '{0}'", dataType));
            }
        }

        private DataStreamPluginWrapper()
        {
            manager = new PluginManager<IDataStreamFactory>();
        }
    }
}

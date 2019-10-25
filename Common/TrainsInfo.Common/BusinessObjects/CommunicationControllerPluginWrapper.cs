using System;
using TrainsInfo.Common.Interfaces;

namespace TrainsInfo.Common.BusinessObjects
{
    public class CommunicationControllerPluginWrapper
    {
        private static readonly CommunicationControllerPluginWrapper instance;

        public static CommunicationControllerPluginWrapper Instance
        {
            get
            {
                return instance;
            }
        }

        static CommunicationControllerPluginWrapper()
        {
            instance = new CommunicationControllerPluginWrapper();
            instance.manager.Load(AppDomain.CurrentDomain.BaseDirectory, "TrainsInfo.DataStream.Communication.Controller.*.dll");
        }

        private PluginManager<ICommunicationControllerFactory> manager;

        public ICommunicationControllerFactory this[string dataType]
        {
            get
            {
                return instance.manager.Plugins[dataType];
            }
        }

        private CommunicationControllerPluginWrapper()
        {
            manager = new PluginManager<ICommunicationControllerFactory>();
        }
    }
}

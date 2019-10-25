using System;
using TrainsInfo.Configuration.Records;
using TrainsInfo.Common.Interfaces;
using TrainsInfo.Common.Attributes;

namespace TrainsInfo.DataStream.Communication.Controller.Dsccp
{
    [PluginType("Dsccp")]
    public class DsccpControllerFactory : ICommunicationControllerFactory
    {
        public ICommunicationController Create(SettingableRecord settings, IDataStream client, IServer server)
        {
            return new DsccpController(client, server);
        }
    }
}

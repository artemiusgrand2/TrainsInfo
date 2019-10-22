using System;
using System.Collections.Generic;
using TrainsInfo.Common.Attributes;
using TrainsInfo.Common.Interfaces;
using TrainsInfo.Configuration.Records;

namespace TrainsInfo.DataStreams.ARDP
{
    [PluginType("ARDP")]
    public class ARDPDataStreamFactory : IDataStreamFactory
    {
        public IDataStream CreateClientStream(DataStreamRecord record)
        {
            return new ARDPDataStream(record);
        }
    }
}

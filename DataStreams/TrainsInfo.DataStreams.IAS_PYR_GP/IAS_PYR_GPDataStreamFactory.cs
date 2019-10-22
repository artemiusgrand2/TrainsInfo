using System;
using System.Collections.Generic;
using TrainsInfo.Common.Attributes;
using TrainsInfo.Configuration.Records;
using TrainsInfo.Common.Interfaces;

namespace TrainsInfo.DataStreams.IAS_PYR_GP
{
    [PluginType("IAS_PYR_GP")]
    public class IAS_PYR_GPDataStreamFactory : IDataStreamFactory
    {
        public IDataStream CreateClientStream(DataStreamRecord record)
        {
            return new IAS_PYR_GPDataStream(record);
        }
    }
}

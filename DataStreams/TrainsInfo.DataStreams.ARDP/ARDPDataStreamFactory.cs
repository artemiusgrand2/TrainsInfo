using System;
using System.Collections.Generic;
using TrainsInfo.Common.Attributes;
using TrainsInfo.Common.Interfaces;
using TrainsInfo.Configuration.Records;

namespace TrainsInfo.DataStream.ARDP
{
    [PluginType("ARDP")]
    public class ARDPDataStreamFactory : IDataStreamFactory
    {
        public IDataStream CreateClientStream(DataStreamRecord record)
        {
            return new ARDPDataStream(record);
        }

        public IListener CreateListener(ListenerRecord record)
        {
            throw new NotSupportedException("ARDP support listeners");
        }
    }
}

using System;
using System.Collections.Generic;
using TrainsInfo.Common.Attributes;
using TrainsInfo.Configuration.Records;
using TrainsInfo.Common.Interfaces;

namespace TrainsInfo.DataStream.Http
{
    [PluginType("Http")]
    public class HttpDataStreamFactory : IDataStreamFactory
    {
        public IDataStream CreateClientStream(DataStreamRecord record)
        {
            return null;
        }

        public IListener CreateListener(ListenerRecord record)
        {
            return new HttpDataStreamListener(record);
        }
    }
}

using System;
using System.Collections.Generic;
using TrainsInfo.Common.Attributes;
using TrainsInfo.Configuration.Records;
using TrainsInfo.Common.Interfaces;

namespace TrainsInfo.DataStreams.File
{
    [PluginType("File")]
    public class FileDataStreamFactory : IDataStreamFactory
    {
        public IDataStream CreateClientStream(DataStreamRecord record)
        {
            return new FileDataStream(record);
        }
    }
}

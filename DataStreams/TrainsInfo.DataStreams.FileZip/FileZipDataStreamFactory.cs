using System;
using System.Collections.Generic;
using TrainsInfo.Common.Attributes;
using TrainsInfo.Configuration.Records;
using TrainsInfo.Common.Interfaces;

namespace TrainsInfo.DataStreams.FileZip
{
    [PluginType("FileZip")]
    public class FileZipDataStreamFactory : IDataStreamFactory
    {
        public IDataStream CreateClientStream(DataStreamRecord record)
        {
            return new FileZipDataStream(record);
        }
    }
}

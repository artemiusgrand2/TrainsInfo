using System;
using System.Collections.Generic;
using TrainsInfo.Common.Attributes;
using TrainsInfo.Configuration.Records;
using TrainsInfo.Common.Interfaces;

namespace TrainsInfo.DataStream.FileZip
{
    [PluginType("FileZip")]
    public class FileZipDataStreamFactory : IDataStreamFactory
    {
        public IDataStream CreateClientStream(DataStreamRecord record)
        {
            return new FileZipDataStream(record);
        }

        public IListener CreateListener(ListenerRecord record)
        {
            throw new NotSupportedException("FileZip support listeners");
        }
    }
}

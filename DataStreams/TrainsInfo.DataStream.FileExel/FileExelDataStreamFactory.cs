using System;
using System.Collections.Generic;
using TrainsInfo.Common.Attributes;
using TrainsInfo.Configuration.Records;
using TrainsInfo.Common.Interfaces;

namespace TrainsInfo.DataStream.FileExel
{
    [PluginType("FileExel")]
    public class FileExelDataStreamFactory : IDataStreamFactory
    {
        public IDataStream CreateClientStream(DataStreamRecord record)
        {
            return new FileExelDataStream(record);
        }

        public IListener CreateListener(ListenerRecord record)
        {
            throw new NotSupportedException("File support listeners");
        }
    }
}

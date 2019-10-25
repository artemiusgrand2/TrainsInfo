using System;
using System.IO;
using System.Collections.Generic;
using TrainsInfo.Common.Attributes;
using TrainsInfo.Configuration.Records;
using TrainsInfo.Common.Interfaces;

namespace TrainsInfo.DataStream.ARDP
{
    public class ARDPDataStream : IDataStream
    {
        public bool IsOnceConnect { get; }

        public ARDPDataStream(DataStreamRecord record)
        {

        }

        public bool Read(out object data)
        {
            data = null;
            return false;
        }

        public int Write(object data)
        {
            return 0;
        }

        public void Dispose() { }

        public string Info { get; } = string.Empty;
    }

}

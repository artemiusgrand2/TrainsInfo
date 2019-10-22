using System;
using System.IO;
using System.Collections.Generic;
using TrainsInfo.Common.Attributes;
using TrainsInfo.Configuration.Records;
using TrainsInfo.Common.Interfaces;

namespace TrainsInfo.DataStreams.ARDP
{
    public class ARDPDataStream : IDataStream
    {

        public ARDPDataStream(DataStreamRecord record)
        {

        }

        public bool Read(out object data)
        {
            data = null;
            return false;
        }

        public void Dispose() { }
    }

}

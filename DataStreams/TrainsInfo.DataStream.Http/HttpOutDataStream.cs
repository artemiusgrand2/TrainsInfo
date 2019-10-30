using System;
using System.Net;
using System.IO;
using System.Text;
using System.Collections.Generic;
using TrainsInfo.Configuration.Records;
using TrainsInfo.Common.Interfaces;

namespace TrainsInfo.DataStream.Http
{
    public class HttpOutDataStream : IDataStream
    {
        private readonly WebRequest request;

        public string Info
        {
            get
            {
                return string.Empty;
            }
        }

        public bool IsOnceConnect { get; } 

        internal HttpOutDataStream(DataStreamRecord httpRecord)
        {
            if (Uri.IsWellFormedUriString(httpRecord.ConnectionString, UriKind.Absolute))
                request = WebRequest.Create(httpRecord.ConnectionString);
            else
                throw new InvalidDataException("incorrect url");
            //
            request.Timeout = 20000;
        }

        public bool Read(out object data)
        {
            var result = new StringBuilder();
            lock (request)
            {
                WebResponse response = request.GetResponse();

                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string line = "";
                        while ((line = reader.ReadLine()) != null)
                            result.Append(line);
                    }
                }
            }
            //
            data = result.ToString();
            return true;
        }

        public int Write(object data)
        {
            return 0;
        }

        public void Dispose()
        {
        }
    }
}
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
        private string url;

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
                url = httpRecord.ConnectionString;
            else
                throw new InvalidDataException("incorrect url");
        }

        public bool Read(out object data)
        {
            var result = new StringBuilder();
            var request = WebRequest.Create(url);
            request.Timeout = 30000;
            lock (request)
            {
                using (var response = request.GetResponse())
                {
                    using (var stream = response.GetResponseStream())
                    {
                        using (var reader = new StreamReader(stream))
                        {
                            string line = "";
                            while ((line = reader.ReadLine()) != null)
                                result.Append(line);
                        }
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
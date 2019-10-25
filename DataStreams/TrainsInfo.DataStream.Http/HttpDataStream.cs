using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Text;
using TrainsInfo.Common.Interfaces;

namespace TrainsInfo.DataStream.Http
{
    public class HttpDataStream : IDataStream
    {
        private readonly HttpListenerContext httpContext;

        public string Info
        {
            get
            {
                return httpContext.Request.RemoteEndPoint.ToString();
            }
        }

        public bool IsOnceConnect { get; } = true;

        internal HttpDataStream(HttpListenerContext httpContext)
        {
            this.httpContext = httpContext;
        }

        public bool Read(out object data)
        {
            lock (httpContext.Request)
            {
                data = httpContext.Request.QueryString;
                return true;
            }
        }

        public int Write(object data)
        {
            int sent;
            lock (httpContext.Response)
            {
                var buffer = (byte[])data;
                httpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");
                httpContext.Response.ContentLength64 = buffer.Length;
                using (var output = httpContext.Response.OutputStream)
                {
                    output.Write(buffer, 0, buffer.Length);
                    output.Close();
                    sent = buffer.Length;
                }
            }
            return sent;
        }

        public void Dispose()
        {
        }
    }
}

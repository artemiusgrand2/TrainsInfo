using System;
using System.Net;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using TrainsInfo.Configuration.Records;
using TrainsInfo.Common.Interfaces;

namespace TrainsInfo.DataStream.Http
{
    public class HttpDataStreamListener : IListener
    {
        private const string BindPort = "BindPort";
        HttpListener listener;
        int listenPort = 8888;

        public HttpDataStreamListener(ListenerRecord record)
        {
            ParseEndPoint(record[BindPort]);
            Init();
        }

        private void Init()
        {
            listener = new HttpListener();
            listener.Prefixes.Add(string.Format("http://*:{0}/", listenPort));
            listener.Prefixes.Add(string.Format("http://localhost:{0}/", listenPort));
            listener.Start();
        }

        private  void ParseEndPoint(string bindPort)
        {
            if (!int.TryParse(bindPort, out listenPort))
            {
                throw new ArgumentException(string.Format("BindPort '{0}' is incorrect", bindPort));
            }
        }

        public IDataStream Accept()
        {
            return new HttpDataStream(listener.GetContext());
        }

        public void Dispose()
        {
            listener.Stop();
            listener.Close();
        }

    }
}

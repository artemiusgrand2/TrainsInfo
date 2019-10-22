using System;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using TrainsInfo.Configuration.Records;

namespace TrainsInfo.Configuration
{
    [XmlRoot(ElementName = "Configuration")]
    [Serializable]
    public class ServerConfiguration
    {
        private static object locker = new object();
        private static ServerConfiguration instance;

        public static ServerConfiguration Instance
        {
            get
            {
                if (instance == null)
                {
                    throw new Exception("Configuration has not been initialized");
                }
                return instance;
            }
        }

        [XmlElement("Logger")]
        public LoggerRecord Logger { get; set; }

        [XmlArray("DataSources")]
        [XmlArrayItem("DataSource")]
        public DataSourceRecord[] DataSources { get; set; }

        [XmlElement("DataStorage")]
        public DataStorageRecord DataStorage { get; set; }

        [XmlArray("Infrastructures")]
        [XmlArrayItem("Infrastructure")]
        public InfrastructureRecord[] Infrastructures { get; set; }


        public static void Initialize(string path)
        {
            if (instance == null)
            {
                lock (locker)
                {
                    if (instance == null)
                    {
                        using (XmlReader xmlReader = XmlReader.Create(path))
                        {
                            instance =
                                (ServerConfiguration)
                                new XmlSerializer(typeof(ServerConfiguration)).Deserialize(xmlReader);
                        }
                    }
                }
            }
        }
    }
}

using System;
using System.Text;
using System.Xml.Serialization;

namespace TrainsInfo.Configuration.Records
{
    [Serializable]
    public class DataStreamRecord
    {
        [XmlElement("ConnectionString")]
        public string ConnectionString { get; set; }

        [XmlElement("Type")]
        public string Type { get; set; }
    }
}

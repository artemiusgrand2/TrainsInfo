using System;
using System.Text;
using System.Xml.Serialization;

namespace TrainsInfo.Configuration.Records
{
    public class DataStorageRecord
    {
        [XmlElement("ConnectionString")]
        public string ConnectionString { get; set; }
    }
}

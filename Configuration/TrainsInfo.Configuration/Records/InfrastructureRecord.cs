using System;
using System.Text;
using System.Xml.Serialization;

namespace TrainsInfo.Configuration.Records
{
    [Serializable]
    public class InfrastructureRecord
    {
        [XmlElement("Type")]
        public string Type { get; set; }
        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("StationCode1")]
        public int StationCode1 { get; set; }

        [XmlElement("StationCode2")]
        public int StationCode2 { get; set; }

        [XmlArray("DataSources")]
        [XmlArrayItem("DataSource")]
        public string[] DataSources { get; set; }

        [XmlArray("StationCode")]
        [XmlArrayItem("StationCode")]
        public string[] StationCodes { get; set; }
    }
}

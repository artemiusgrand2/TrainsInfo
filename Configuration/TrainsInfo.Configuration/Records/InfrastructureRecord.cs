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

        [XmlElement("Station1")]
        public int Station1 { get; set; }

        [XmlElement("Station2")]
        public int Station2 { get; set; }

        [XmlArray("DataSources")]
        [XmlArrayItem("DataSource")]
        public string[] DataSources { get; set; }

        [XmlArray("Areas")]
        [XmlArrayItem("Area")]
        public string[] Areas { get; set; }

        [XmlArray("StationCodes")]
        [XmlArrayItem("StationCode")]
        public string[] StationCodes { get; set; }

        [XmlArray("Nodes")]
        [XmlArrayItem("Node")]
        public NodeRecord [] Nodes { get; set; }
    }
}

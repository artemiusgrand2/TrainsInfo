using System;
using System.Text;
using System.Xml.Serialization;

namespace TrainsInfo.Configuration.Records
{
    [Serializable]
    public class NodeRecord
    {
        [XmlElement("Station")]
        public string Station { get; set; }

        [XmlElement("StationDirection")]
        public string StationDirection { get; set; }

    }
}

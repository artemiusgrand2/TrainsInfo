using System;
using System.Xml.Serialization;

namespace TrainsInfo.Configuration.Records
{

    [Serializable]
    public class SettingRecord
    {
        [XmlAttribute("Key")]
        public string Key { get; set; }

        [XmlAttribute("Value")]
        public string Value { get; set; }
    }
}

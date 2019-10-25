using System;
using System.Text;
using System.Xml.Serialization;

namespace TrainsInfo.Configuration.Records
{
    [Serializable]
    public class DataSourceRecord
    {
        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("DataStream")]
        public DataStreamRecord DataStream { get; set; }

        /// <summary>
        /// интервал обновления в миллисекундах
        /// </summary>
        [XmlElement("RequestTimeout")]
        public uint RequestTimeout { get; set; } = 10000;

        [XmlArray("DataParsers")]
        [XmlArrayItem("DataParser")]
        public string[] DataParsers { get; set; }
    }
}

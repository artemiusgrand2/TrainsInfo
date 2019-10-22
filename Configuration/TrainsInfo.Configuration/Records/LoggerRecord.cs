using System;
using System.Text;
using System.Xml.Serialization;

namespace TrainsInfo.Configuration.Records
{
    [Serializable]
    public class LoggerRecord : SettingableRecord
    {
        [XmlElement("Type")]
        public string Type { get; set; }

        public override string GetDiagnosticsInfo()
        {
            var result = new StringBuilder();
            result.AppendLine("Тег - 'Type' - тип лога, бывает 'log4net'");
            //
            return result.ToString();
        }

    }
}

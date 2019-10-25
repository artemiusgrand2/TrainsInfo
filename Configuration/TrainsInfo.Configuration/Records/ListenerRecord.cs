using System;
using System.Text;
using System.Xml.Serialization;


namespace TrainsInfo.Configuration.Records
{
    [Serializable]
    public class ListenerRecord : SettingableRecord
    {
        [XmlElement("Type")]
        public string Type { get; set; }

        [XmlElement("CommunicationProtocol")]
        public string CommunicationProtocol { get; set; }


        public override string GetDiagnosticsInfo()
        {
            var result = new StringBuilder();
            result.AppendLine("Тег - 'Type' -  тип слушателя, бывает - 'Tcp' (слушает Tcp порты)");
            //result.AppendLine("Тег - 'Frames' - вид алгоритма работы с подлюченными клиентами, бывает - 'SHF' используется только совместно с 'CommunicationProtocol' равным - 'Dsccp';");
            //result.AppendLine("                                                                   'Uni' используется только совместно с 'CommunicationProtocol' равным - 'IS'");
            //result.AppendLine("Тег - 'CommunicationProtocol' - тип протокола между сервером и клиентом, бывает: 'Dsccp' (передает цифровые и аналоговые данные, текущие их значение а также архив),");
            //result.AppendLine("                                                                                 'IS'    (передает цифровые данные в формате импульс сервера),");
            //
            return result.ToString();
        }
    }
}

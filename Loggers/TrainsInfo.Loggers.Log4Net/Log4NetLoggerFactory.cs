using System.IO;
using TrainsInfo.Common.Attributes;
using TrainsInfo.Common.Interfaces;
using TrainsInfo.Configuration.Records;
using log4net;
using log4net.Config;


namespace TrainsInfo.Logger.Log4Net
{
    [PluginType("log4net")]
    public class Log4NetLoggerFactory : ILoggerFactory
    {
        private const string LogPathKey = "ConfigFilePath";
        private const string ClientsPathKey = "ClientsFolder";

        public ILogger Create(LoggerRecord record)
        {
            string path = record[LogPathKey];
            string pathClientInfo = string.Empty;
            //
            if (!File.Exists(path))
                throw new System.Exception(string.Format("Файла конфигуратора лога по адресу - {0} не существует", path));
            //
            XmlConfigurator.Configure(new FileInfo(path));
            ILog log = LogManager.GetLogger("RollingFileLogger");
            var result = new Log4NetLoggerWrapper(log);
            //
            if (record.TryGetSetting(ClientsPathKey, out pathClientInfo))
            {
                //if (!File.Exists(pathClientInfo))
                //    result.LogInfo(string.Format("Файла журнала подключений клиентов по адресу - {0} не существует", pathClientInfo), null);
                result.PathClientInfo = pathClientInfo;
            }
            else
                result.LogInfo(string.Format("Тег - 'ClientsFolder' для журнала подключений клиентов не описан", pathClientInfo), null);
            //
            return result;
        }
    }
}

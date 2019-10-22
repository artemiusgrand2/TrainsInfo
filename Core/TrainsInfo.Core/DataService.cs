using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Configuration.Install;

namespace TrainsInfo.Core
{
    public class DataService : ServiceBase
    {

        protected override void OnStart(string[] args)
        {
            Program.Start();
        }

        protected override void OnStop()
        {
            Program.Stop();
        }

    }

    // <summary>
    /// Класс установщика
    /// </summary>
    [RunInstallerAttribute(true)]
    public class TrainsInfoServerInstaller : Installer
    {
        private ServiceInstaller m_serviceInstaller;
        private ServiceProcessInstaller m_processInstaller;

        /// <summary>
        /// Конструктор
        /// </summary>
        public TrainsInfoServerInstaller()
        {
            m_serviceInstaller = new ServiceInstaller();
            m_processInstaller = new ServiceProcessInstaller();

            m_processInstaller.Account = ServiceAccount.LocalSystem;
            m_serviceInstaller.StartType = ServiceStartMode.Automatic;

            m_serviceInstaller.ServiceName = "TrainsInfoServer";
            m_serviceInstaller.Description = "Сервер расчета показателей схемы оперативной поездной обстановки";
            m_serviceInstaller.DisplayName = "TrainsInfoServer";

            m_serviceInstaller.ServicesDependedOn = new string[1];
            m_serviceInstaller.ServicesDependedOn[0] = "tcpip";

            Installers.Add(m_serviceInstaller);
            Installers.Add(m_processInstaller);
        }
    }
}

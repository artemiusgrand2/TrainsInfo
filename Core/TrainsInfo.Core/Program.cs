using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Reflection;
using System.Configuration.Install;
using System.ServiceProcess;
using TrainsInfo.Configuration;
using TrainsInfo.Common.Enums;
using TrainsInfo.Common.Infrastructures;
using TrainsInfo.Common.Interfaces;
using TrainsInfo.Core.BusinessObjects;
using TrainsInfo.Common.BusinessObjects;
using TrainsInfo.Configuration.Records;

namespace TrainsInfo.Core
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Initializing...");
            //Console.ReadLine();
            var m_runAsConsole = false;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
            System.Diagnostics.Process.GetCurrentProcess().PriorityClass = System.Diagnostics.ProcessPriorityClass.High;
            ServerContextWrapper.Instance.ConfigPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\ServerConfiguration.xml";
            //разбираю командную строку
            for (int i = 0; i < args.Length; i++)
            {
                if (string.Compare(args[i], "-console", true) == 0 || string.Compare(args[i], "/console", true) == 0 ||
                string.Compare(args[i], "-c", true) == 0 || string.Compare(args[i], "/c", true) == 0)
                {
                    m_runAsConsole = true;
                    continue;
                }
                if (string.Compare(args[i], "-install", true) == 0
                  || string.Compare(args[i], "/install", true) == 0)
                {
                    InstallService();
                    return;
                }
                else if (string.Compare(args[i], "-uninstall", true) == 0
                   || string.Compare(args[i], "/uninstall", true) == 0)
                {
                    UninstallService();
                    return;
                }
                else
                {
                    //нахождение файла конфигурации
                    ServerContextWrapper.Instance.ConfigPath = args[0];
                }
            }
            //
            //

            if (m_runAsConsole)
            {
                Console.CancelKeyPress += CloseEngineramm;
                Start();
            }
            else
                ServiceBase.Run(new DataService());
        }

        private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs args)
        {
            if (Logger.Log != null)
            {
                Logger.Log.LogError("Unhandled error. {0}", args.ExceptionObject);
            }
            else
            {
                Console.WriteLine("Unhandled error. {0}", args.ExceptionObject);
            }
        }

        private static void InfoToExit()
        {
            Console.WriteLine("Произошла критическая ошибка. Смотри лог.");
            Console.WriteLine("Нажмите клавишу для выхода...");
            Console.ReadLine();
            Environment.Exit(int.MaxValue);
        }

        private static void LoadLogger()
        {
            Logger.Init(ServerConfiguration.Instance.Logger, (LogLevel)Enum.Parse(
                        typeof(LogLevel),
                        ServerConfiguration.Instance.Logger["LogLevel"]));
        }

        private static void CloseEngineramm(object sender, ConsoleCancelEventArgs e)
        {
            Stop();
        }

        public static void Start()
        {
            IList<string> errors = new List<string>();
            try
            {
                ServerConfiguration.Initialize(ServerContextWrapper.Instance.ConfigPath);
                LoadLogger();
                AppDomain.CurrentDomain.AssemblyLoad +=
                    (sender, eventArgs) =>
                    {
                        Logger.Log.LogInfo("{0} loaded.", eventArgs.LoadedAssembly.FullName);
                    };
                AppDomain.CurrentDomain.UnhandledException += (sender, eventArgs) =>
                {
                    Logger.Log.LogError("Unhadled exception. {0}", eventArgs.ExceptionObject);
                    InfoToExit();
                };
                Logger.Log.LogInfo("Запуск сервера");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                Console.WriteLine("Нажмите клавишу для выхода...");
                Console.ReadLine();
                Environment.Exit(int.MaxValue);
            }
            //
            try
            {
                FillDataSources(errors);
                ServerContextWrapper.Instance.DataStorage = new DataStorage(ServerConfiguration.Instance.DataStorage.ConnectionString);
                PrintErrors(errors);
                FillStations(errors);
                AssignTablesToStorages();
                //
                ServerContextWrapper.Instance.DataStorage.Start();
                foreach (DataSource dataSource in ServerContextWrapper.Instance.DataSources.Values)
                    dataSource.Start();
                //
                Console.WriteLine("Data Server [{0}] is started", Assembly.GetCallingAssembly().GetName().Version);
                Logger.Log.LogInfo("Data Server [{0}] is started", Assembly.GetCallingAssembly().GetName().Version);
            }
            catch (Exception e)
            {
                errors.Add(string.Format("{0}", e));
            }
        }

        public static void Stop()
        {
            try
            {
                Console.WriteLine("Wait DataServer stop");
                if (ServerContextWrapper.Instance.DataStorage != null)
                    ServerContextWrapper.Instance.DataStorage.Stop();
                //
                foreach (DataSource dataSource in ServerContextWrapper.Instance.DataSources.Values)
                    dataSource.Stop();
                Console.WriteLine("Data Server [{0}] is end", Assembly.GetCallingAssembly().GetName().Version);
                Logger.Log.LogInfo("Data Server [{0}] is end", Assembly.GetCallingAssembly().GetName().Version);
                Console.WriteLine("Нажмите клавишу для выхода...");
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Logger.Log.LogError("{0}", e.Message);
            }
        }

        private static void InstallService()
        {
            AssemblyInstaller installer = new AssemblyInstaller();
            IDictionary savedState = new Hashtable();
            string[] commandLine = { "/logFile=TrainsInfoServerCheck.log" };

            installer.Assembly = Assembly.GetExecutingAssembly();
            installer.CommandLine = commandLine;
            installer.UseNewContext = true;
            try
            {
                installer.Install(savedState);
            }
            catch
            {
                installer.Rollback(savedState);
                return;
            }
            finally
            {
                //Console.WriteLine("Нажмите любую клавишу для выхода...");
                //Console.ReadLine();
            }
        }

        private static void UninstallService()
        {
            AssemblyInstaller installer = new AssemblyInstaller();
            IDictionary savedState = new Hashtable();
            string[] commandLine = { "/logFile=TrainsInfoServerCheck.log" };

            installer.Assembly = Assembly.GetCallingAssembly();
            installer.UseNewContext = true;
            installer.CommandLine = commandLine;
            try
            {
                installer.Uninstall(savedState);
            }
            catch
            {
                return;
            }
            finally
            {
                //Console.WriteLine("Нажмите любую клавишу для выхода...");
                //Console.ReadLine();
            }
        }

        private static void AssignTablesToStorages()
        {
            foreach (var dataSource in ServerContextWrapper.Instance.DataSources)
            {
                if(ServerContextWrapper.Instance.DataStorage != null)
                {
                    dataSource.Value.NewValues += (newValues) =>
                        {
                            ServerContextWrapper.Instance.DataStorage.ProcessValueChanged(newValues);
                        };
                }
            }
        }


        private static void PrintErrors(IList<string> errors)
        {
            if (errors.Count > 0)
            {
                foreach (string str in errors)
                {
                    Logger.Log.LogError("{0}", str);
                }
                InfoToExit();
            }
        }


        private static void FillStations(IList<string> errors)
        {
            foreach (var infrastructurRecord in ServerConfiguration.Instance.Infrastructures)
            {
                var infrastructure = InfrastructureFactory.Create(infrastructurRecord);
                if(infrastructurRecord != null)
                {
                    ServerContextWrapper.Instance.Infrastructures.Add(infrastructure);
                    foreach (string dataSource in infrastructurRecord.DataSources)
                    {
                        if (ServerContextWrapper.Instance.DataSources.ContainsKey(dataSource))
                            ServerContextWrapper.Instance.DataSources[dataSource].AddInfrastructure(infrastructure);
                        else
                            errors.Add(string.Format("Источник 'dataSource' с именем - {0} не описан, инфраструктура с именем - {1}", dataSource, infrastructurRecord.Name));
                    }
                }
            }
        }

        private static void FillDataSources(IList<string> errors)
        {
            foreach (DataSourceRecord dataSourceRecord in ServerConfiguration.Instance.DataSources)
            {
                IDataStream stream =
                    DataStreamPluginWrapper.Instance[dataSourceRecord.DataStream.Type].CreateClientStream(
                        dataSourceRecord.DataStream);

                var parsers = new List<IDataParser>();
                foreach(var parserReccord in dataSourceRecord.DataParsers)
                    parsers.Add(DataParserPluginWrapper.Instance[parserReccord].Create());
                var dataSource = new DataSource(stream, parsers, dataSourceRecord.RequestTimeout);
                if (!ServerContextWrapper.Instance.DataSources.ContainsKey(dataSourceRecord.Name))
                    ServerContextWrapper.Instance.DataSources.Add(dataSourceRecord.Name, dataSource);
                else
                    errors.Add(string.Format("источник с именем - '{0}' повторяется", dataSourceRecord.Name));
            }
        }
    }
}

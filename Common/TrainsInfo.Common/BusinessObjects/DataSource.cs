using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web.Script.Serialization;
using TrainsInfo.Common.Interfaces;
using TrainsInfo.Common.Infrastructures;

namespace TrainsInfo.Common.BusinessObjects
{
    public  class DataSource
    {
        private IList<DataSource> dataStreamHeirdom = new List<DataSource>();
        delegate void WriteLog(string format, params object[] args);
        private readonly IDataStream dataStream;
        private readonly IList<IDataParser> dataParsers;
        private readonly Thread parsingThread;
        private readonly uint requestTimeout = 0;
        private bool isStop;
        private readonly string nameSource;

        public IDataStream DataStream
        {
            get
            {
                return dataStream;
            }
        }

        private IList<InfrastructureBase> infrastructures;

        public IList<InfrastructureBase> Infrastructures
        {
            get
            {
                return infrastructures;
            }
        }


        public event NewValueHandler<IList<RowValue>> NewValues;

        public DataSource(IDataStream stream, IList<IDataParser> parsers,  uint RequestTimeout, string nameSource)
        {
            parsingThread = new Thread(Parse)
            {
                Priority = ThreadPriority.Highest
            };
            //
            dataStream = stream;
            dataParsers = parsers;
            requestTimeout = (RequestTimeout > 0) ? RequestTimeout : 15;
            this.nameSource = nameSource;
            infrastructures = new List<InfrastructureBase>();
        }

        public void Start()
        {
            if (!isStop)
            {
                isStop = false;
                parsingThread.Start();
            }
        }

        public void Stop()
        {
            isStop = true;
            parsingThread.Join(2000);
        }

        public void AddInfrastructure(InfrastructureBase infrastructure)
        {
            infrastructures.Add(infrastructure);
        }

        private void Parse()
        {
            while (!isStop)
            {
                try
                {
                    object data;
                    if(dataStream.Read(out data))
                    {
                        WriteLog writeLog = null;
                        //для поездов, полученных из IAS_PYR_GP, AGDP
                        if (nameSource.ToUpper().IndexOf("IAS_PYR_GP") != -1 || nameSource.ToUpper().IndexOf("AGDPSOURCE") != -1)
                        {
                            if (nameSource.ToUpper().IndexOf("IAS_PYR_GP") != -1)
                                writeLog = Logger.Log.LogInfo;
                            else
                            {
                                data = ((new JavaScriptSerializer()).Deserialize<IList<ModelAGDP>>(data as string)).Select(x=>x as ModelBase).ToList();
                                writeLog = Logger.Log.AreaSubTrainsInfo;
                            }
                            //
                            writeLog("------------------------------------------------------------------------------------------------", null);
                            writeLog("", null);
                            writeLog("Новая иттерация получения данных с источника - {0}", nameSource);
                            writeLog("", null);
                            writeLog("------------------------------------------------------------------------------------------------", null);
                            //
                            if (writeLog == Logger.Log.LogInfo)
                                writeLog = Logger.Log.OthersTrainsInfo;
                        }
                        //
                        var parserValues = new List<RowValue>();
                        foreach (var parser in dataParsers)
                            parserValues.AddRange(parser.Parse(data, infrastructures));
                        //
                        WriteNotApplyTrain(data,  writeLog);
                        //
                        if (parserValues.Count > 0)
                            OnNewValues(parserValues);
                    }
                }
                catch (Exception e)
                {
                    Logger.Log.LogError("Error parsing message. {0}", e);
                }
                finally
                {
                    Thread.Sleep((int)requestTimeout);
                }
            }
        }

        private void WriteNotApplyTrain(object data, WriteLog writeLog)
        {
            var table = data as IList<ModelBase>;
            if (table != null)
            {
                var notApplyTrains = table.Where(x => !x.IsApply);
                writeLog("------------------------------------------------------------------------------------------------", null);
                writeLog("", null);
                writeLog("Новая иттерация получения данных с источника - {0}, перечень неспользуемых поездов (всего - {1}, неспользуемых - {2})", nameSource, table.Count, notApplyTrains.Count());
                writeLog("", null);
                writeLog("------------------------------------------------------------------------------------------------", null);
                //
                var index = 1;
                foreach (var trainEvent in notApplyTrains)
                {
                    writeLog("{0}. {1}", index, new JavaScriptSerializer().Serialize(trainEvent));
                    index++;
                }
            }
        }

        private void OnNewValues(IList<RowValue> values)
        {
            NewValueHandler<IList<RowValue>> handler = NewValues;
            if (handler != null)
            {
                handler(values);
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TrainsInfo.Common.Interfaces;
using TrainsInfo.Common.Infrastructures;

namespace TrainsInfo.Common.BusinessObjects
{
    public  class DataSource
    {
        private IList<DataSource> dataStreamHeirdom = new List<DataSource>();

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
                        if (nameSource.ToUpper().IndexOf("IAS_PYR_GP") != -1)
                        {
                            Logger.Log.LogInfo("------------------------------------------------------------------------------------------------", null);
                            Logger.Log.LogInfo("", null);
                            Logger.Log.LogInfo("Новая итерация получения данных с источника - {0}", nameSource);
                            Logger.Log.LogInfo("", null);
                            Logger.Log.LogInfo("------------------------------------------------------------------------------------------------", null);
                        }

                        var parserValues = new List<RowValue>();
                        foreach (var parser in dataParsers)
                            parserValues.AddRange(parser.Parse(data, infrastructures));
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

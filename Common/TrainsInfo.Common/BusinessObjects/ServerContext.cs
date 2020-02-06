using System;
using System.Linq;
using System.Collections.Generic;
using TrainsInfo.Common.Interfaces;
using TrainsInfo.Common.Enums;
using TrainsInfo.Common.Infrastructures;

namespace TrainsInfo.Common.BusinessObjects
{
    public class ServerContext : IServer
    {
        public string ConfigPath { get; set; }

        public IDictionary<string, DataSource> DataSources { get; private set; }
        public IList<InfrastructureBase> Infrastructures { get; private set; }
        public IList<RowValue> Values { get; private set; }
        public IList<ListenerController> Listeners { get; private set; }

        private readonly IDictionary<CategotyTable, IList<string>> dictionaryCategotyTable = new Dictionary<CategotyTable, IList<string>>()

        {
            {CategotyTable.left, new List<string> {Constants.KeyTablePassOpz, Constants.KeyTableVostok_Zapad, Constants.KeyTableZapad_Vostok, Constants.KeyTableBroshPoezd } },

            {CategotyTable.right, new List<string> {Constants.KeyTableGruzRabota, Constants.KeyTableLok_nod, Constants.KeyTableLok_ojto2, Constants.KeyTableLok_styk,
                                                    Constants.KeyTablePeredacha, Constants.KeyTableRazvoz, Constants.KeyTableREG_TAB,  Constants.KeyTableREG_TAB_PLAN} }
        };


        public DataStorage DataStorage { get; set; }
        public event NewValueHandler<IList<RowValue>> NewValues;

        public ServerContext()
        {
            DataSources = new Dictionary<string, DataSource>();
            Infrastructures = new List<InfrastructureBase>();
            Values = new List<RowValue>();
            Listeners = new List<ListenerController>();
        }

        public IDictionary<string, BaseValue> GetTables(CategotyTable categoty)
        {
            IList<string> keys;
            if(dictionaryCategotyTable.TryGetValue(categoty, out keys))
            {
               return  Values.Where(x => keys.Contains(x.Name)).ToDictionary(x => x.Name, x => new BaseValue(x.Value, x.LastUpdate));
            }
            return new Dictionary<string, BaseValue>();
        }

        public void CompareData(IList<RowValue> data)
        {
            lock (Values)
            {
                var newValues = new List<RowValue>();
                foreach (var newValue in data)
                {
                    var find = Values.Where(x => x.Station1 == newValue.Station1 && x.Station2 == newValue.Station2 && x.Name == newValue.Name).FirstOrDefault();
                    if (find != null)
                    {
                        if (newValue.Value != find.Value)
                        {
                            ComparePassOp(find, newValue);
                            newValues.Add(newValue);
                            find.Value = newValue.Value;
                        }
                        //
                        if (newValue.LastUpdate != find.LastUpdate)
                            find.LastUpdate = newValue.LastUpdate;
                    }
                    else
                    {
                        Values.Add(newValue);
                        newValues.Add(newValue);
                    }
                }
                //
                if (newValues.Count > 0)
                    OnNewValues(newValues);
            }
        }

        private void ComparePassOp(RowValue oldValue, RowValue newValue)
        {
           
            if(oldValue.Name == Constants.KeyTablePassOpz)
            {
                var updateTrains = new List<ModelTrainPassOpz>();
                foreach (var trainPassPrz in newValue.TrainsPassOpz)
                {
                   var findTrains = oldValue.TrainsPassOpz.Where(x => x.TrainNumber == trainPassPrz.TrainNumber).ToList();
                    if (findTrains.Count > 0)
                    {
                        if (findTrains.Where(x => x.Lateness != trainPassPrz.Lateness || x.StationOperation != trainPassPrz.StationOperation || x.TimeOperation != trainPassPrz.TimeOperation).ToList().Count != 0)
                            updateTrains.Add(trainPassPrz);
                    }
                    else
                        updateTrains.Add(trainPassPrz);
                }
                //
                oldValue.TrainsPassOpz = newValue.TrainsPassOpz;
                newValue.TrainsPassOpz = updateTrains;
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

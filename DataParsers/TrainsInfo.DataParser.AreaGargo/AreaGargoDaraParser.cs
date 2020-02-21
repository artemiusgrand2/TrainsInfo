using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using TrainsInfo.Common.Enums;
using TrainsInfo.Common.BusinessObjects;
using TrainsInfo.Common.Interfaces;
using TrainsInfo.Common.Infrastructures;

namespace TrainsInfo.DataParser.AreaGargo
{
    public class AreaGargoDaraParser : IDataParser
    {
        private readonly string NC_GRTrain = "NC_GR";
        private readonly string C_GRTrain = "C_GR";

        private readonly IList<string> codesOperation1 = new List<string> { "P0002", "P0003", "P0102", "P0042", "P0033", "P0043", "P0031", "P0103" };

        private readonly IList<string> codesOperation2 = new List<string> { "P0001" };

        private readonly IList<string> codesOperation3 = new List<string> { "P0005", "P0009" };

        public IList<RowValue> Parse(object data, IList<InfrastructureBase> infrastructures)
        {
            var table = data as IList<ModelDataIAS_PYR_GP>;
            var result = new List<RowValue>();
            if (table != null)
            {
                foreach (var areaCommon in infrastructures.Where(x => x.Type == TypeInfrastructure.area).ToList())
                {
                    var area = areaCommon as Area;
                    var events = table.Where(x => 
                    (((area.ListStations.Contains(x.StationCode) || area.ListStations.Contains(x.DirectionToStation) || (area.Nodes.Where(y=>y.Key == x.StationCode && y.Value == x.DirectionToStation).Count() != 0)) && 
                    (codesOperation1.Contains(x.OperationCode) || (x.TrainNumber != 9999 && codesOperation3.Contains(x.OperationCode))))
                    || (area.ListStations.Contains(x.StationCode)  && codesOperation2.Contains(x.OperationCode))));
                    //
                    Logger.Log.LogInfo("Участок {0}-{1} грузовые поезда (четные):", area.Station, area.Station2);
                    var index = 1;

                    foreach (var newEvent in events.Where(x => x.TrainNumber % 2 == 0))
                    {
                        Logger.Log.LogInfo("{0}. {1}", index, new JavaScriptSerializer().Serialize(newEvent));
                        newEvent.IsApply = true;
                        index++;
                    }

                    Logger.Log.LogInfo("Участок {0}-{1} грузовые поезда (нечетные):", area.Station, area.Station2);
                    index = 1;
                    //
                    foreach (var newEvent in events.Where(x => x.TrainNumber % 2 != 0))
                    {
                        Logger.Log.LogInfo("{0}. {1}", index, new JavaScriptSerializer().Serialize(newEvent));
                        newEvent.IsApply = true;
                        index++;
                    }
                    result.Add(new RowValue(area.Station, area.Station2, NC_GRTrain, events.Select(x => x.TrainNumber).Where(x => x % 2 != 0).Count().ToString(), DateTime.Now));
                    result.Add(new RowValue(area.Station, area.Station2, C_GRTrain, events.Select(x => x.TrainNumber).Where(x => x % 2 == 0).Count().ToString(), DateTime.Now));
                }
            }
            //
            return result;
        }
    }
}

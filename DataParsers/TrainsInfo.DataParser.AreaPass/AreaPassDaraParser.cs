using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using TrainsInfo.Common.Enums;
using TrainsInfo.Common.BusinessObjects;
using TrainsInfo.Common.Interfaces;
using TrainsInfo.Common.Infrastructures;

namespace TrainsInfo.DataParser.AreaPass
{
    public class AreaPassDaraParser : IDataParser
    {
        private readonly string NC_PSTrain = "NC_PS";
        private readonly string C_PSTrain = "C_PS";

        private readonly IList<string> codesOperationNode = new List<string> {  "C0001", "C0003", "C1020" };
        private readonly IList<string> codesOperationArea = new List<string> { "C0002", "C0042", "C1010", "C0003", "C0043", "C0033" };

        public IList<RowValue> Parse(object data, IList<InfrastructureBase> infrastructures)
        {
            var table = data as IList<ModelDataIAS_PYR_GP>;
            var result = new List<RowValue>();
            if (table != null)
            {
                foreach (var areaCommon in infrastructures.Where(x => x.Type == TypeInfrastructure.area).ToList())
                {
                    var area = areaCommon as Area;
                    if(area.Station == 166403 && area.Station2 == 171346)
                    {

                    }
                    var events = table.Where(x => x.TrainNumber >= 1 && x.TrainNumber <= 1000
                    && (((area.ListStations.Contains(x.StationCode) || area.ListStations.Contains(x.DirectionToStation)) && codesOperationArea.Contains(x.OperationCode))
                    || (area.Nodes.Where(y => y.Key == x.StationCode && y.Value == x.DirectionToStation).Count() != 0 && codesOperationNode.Contains(x.OperationCode))));
                    //
                    Logger.Log.LogInfo("Участок {0}-{1} пассажирские поезда (четные):", area.Station, area.Station2);
                    var index = 1;

                    foreach (var newEvent in events.Where(x=>x.TrainNumber % 2 == 0))
                    {
                        Logger.Log.LogInfo("{0}. {1}", index, new JavaScriptSerializer().Serialize(newEvent));
                        newEvent.IsApply = true;
                        index++;
                    }

                    Logger.Log.LogInfo("Участок {0}-{1} пассажирские поезда (нечетные):", area.Station, area.Station2);
                    index = 1;
                    //
                    foreach (var newEvent in events.Where(x => x.TrainNumber % 2 != 0))
                    {
                        newEvent.IsApply = true;
                        Logger.Log.LogInfo("{0}. {1}", index, new JavaScriptSerializer().Serialize(newEvent));
                        index++;
                    }
                    //
                    result.Add(new RowValue(area.Station, area.Station2, NC_PSTrain, events.Select(x=>x.TrainNumber).Where(x => x % 2 != 0).Count().ToString(), DateTime.Now));
                    result.Add(new RowValue(area.Station, area.Station2, C_PSTrain, events.Select(x => x.TrainNumber).Where(x => x % 2 == 0).Count().ToString(), DateTime.Now));
                }
            }
            //
            return result;
        }
    }
}

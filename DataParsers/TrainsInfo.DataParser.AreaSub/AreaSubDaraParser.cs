using System;
using System.IO;
using System.Linq;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using TrainsInfo.Common.Enums;
using TrainsInfo.Common.BusinessObjects;
using TrainsInfo.Common.Interfaces;
using TrainsInfo.Common.Infrastructures;

namespace TrainsInfo.DataParser.AreaSub
{
    public class AreaSubDaraParser : IDataParser
    {
        private readonly string NC_PRTrain = "NC_PR";
        private readonly string C_PRTrain = "C_PR";

        public IList<RowValue> Parse(object data, IList<InfrastructureBase> infrastructures)
        {
            var table = data as IList<ModelBase>;// data as string;
            var result = new List<RowValue>();
            if (table != null)
            {
               var  tableAGDP = table.Select(x => x as ModelAGDP).ToList();
                // var parserModel =  (new JavaScriptSerializer()).Deserialize<IList<ModelAGDP>>(input);
                // if(parserModel != null)
                //  {
                foreach (var areaCommon in infrastructures.Where(x => x.Type == TypeInfrastructure.area).ToList())
                    {
                        var area = areaCommon as Area;
                        var allSubTrain = tableAGDP.Where(x => area.Areas.Contains(x.AreaNumber));
                        Logger.Log.AreaSubTrainsInfo("Участок {0}-{1} пригородные поезда (четные):", area.Station, area.Station2);
                        var index = 1;
                        foreach (var train in allSubTrain.Where(x => x.TrainNumber % 2 == 0))
                        {
                            train.IsApply = true;
                            Logger.Log.AreaSubTrainsInfo("{0}. {1}", index, new JavaScriptSerializer().Serialize(train));
                            index++;
                        }
                        //
                        index = 1;
                        Logger.Log.AreaSubTrainsInfo("Участок {0}-{1} пригородные поезда (нечетные):", area.Station, area.Station2);
                        foreach (var train in allSubTrain.Where(x => x.TrainNumber % 2 != 0))
                        {
                            train.IsApply = true;
                            Logger.Log.AreaSubTrainsInfo("{0}. {1}", index, new JavaScriptSerializer().Serialize(train));
                            index++;
                        }
                        //
                        result.Add(new RowValue(area.Station, area.Station2, NC_PRTrain, allSubTrain.Where(x => x.TrainNumber % 2 != 0).Count().ToString(), DateTime.Now));
                        result.Add(new RowValue(area.Station, area.Station2, C_PRTrain, allSubTrain.Where(x => x.TrainNumber % 2 == 0).Count().ToString(), DateTime.Now));
                    }
              //  }
            }
            //
            return result;
        }
    }


}

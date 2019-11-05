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
            var input = data as string;
            var result = new List<RowValue>();
            if (input != null)
            {
                var parserModel =  (new JavaScriptSerializer()).Deserialize<IList<ModelAGDP>>(input);
                if(parserModel != null)
                {
                    foreach (var areaCommon in infrastructures.Where(x => x.Type == TypeInfrastructure.area).ToList())
                    {
                        var area = areaCommon as Area;
                        if(area.Areas.Count > 0 && area.Areas.Contains("33"))
                        {

                        }
                        var allTrain = parserModel.Where(x => area.Areas.Contains(x.AreaNumber)).Select(x => x.TrainNumber);
                        //
                        result.Add(new RowValue(area.Station, area.Station2, NC_PRTrain, allTrain.Where(x => x % 2 != 0).Count().ToString(), DateTime.Now));
                        result.Add(new RowValue(area.Station, area.Station2, C_PRTrain, allTrain.Where(x => x % 2 == 0).Count().ToString(), DateTime.Now));
                    }
                }
            }
            //
            return result;
        }
    }

    public class ModelAGDP
    {
        public int TrainNumber { get; set; }
        public string AreaNumber { get; set; }
    }

}

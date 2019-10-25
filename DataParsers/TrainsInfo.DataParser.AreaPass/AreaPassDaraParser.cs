using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
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

        private readonly IList<string> codesOperation = new List<string> {  "C0002", "C0003" };

        public IList<RowValue> Parse(object data, IList<InfrastructureBase> infrastructures)
        {
            var table = data as IList<ModelDataIAS_PYR_GP>;
            var result = new List<RowValue>();
            if (table != null)
            {
                foreach (var areaCommon in infrastructures.Where(x => x.Type == TypeInfrastructure.area).ToList())
                {
                    var area = areaCommon as Area;
                    var allTrain = table.Where(x => x.TrainNumber >= 1 && x.TrainNumber <= 1000
                    && ((area.ListStations.Contains(x.StationCode) || area.ListStations.Contains(x.DirectionToStation) || (area.Nodes.ContainsKey(x.StationCode) && area.Nodes[x.StationCode] == x.DirectionToStation)) && codesOperation.Contains(x.OperationCode))).Select(x => x.TrainNumber);
                    //
                    result.Add(new RowValue(area.Station, area.Station2, NC_PSTrain, allTrain.Where(x => x % 2 != 0).Count().ToString()));
                    result.Add(new RowValue(area.Station, area.Station2, C_PSTrain, allTrain.Where(x => x % 2 == 0).Count().ToString()));
                }
            }
            //
            return result;
        }
    }
}

using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using TrainsInfo.Common.Enums;
using TrainsInfo.Common.BusinessObjects;
using TrainsInfo.Common.Interfaces;
using TrainsInfo.Common.Infrastructures;

namespace TrainsInfo.DataParser.AreaGargo
{
    public class AreaGargoDaraParser : IDataParser
    {
        private readonly string TRTrain = "TR";
        private readonly IList<string> codesOperation = new List<string> { "Р0001", "Р0101", "Р0201", "Р0003", "Р0103" };

        public IList<RowValue> Parse(object data, IList<IInfrastructure> infrastructures)
        {
            var table = data as IList<ModelDataIAS_PYR_GP>;
            var result = new List<RowValue>();
            if (table != null)
            {
                var tableWithFilter = table.Where(x => codesOperation.Contains(x.OperationCode) && x.StationCode != x.Index3 && x.TrainNumber != "9999").ToList();
                foreach (var node in infrastructures.Where(x => x.Type == TypeInfrastructure.node).ToList())
                {
                    var countTrain = tableWithFilter.Where(x => (node as Node).ListStations.Contains(x.StationCode)).ToList();
                    result.Add(new RowValue(node.Station, TRTrain, countTrain.ToString()));
                }
            }
            //
            return result;
        }
    }
}

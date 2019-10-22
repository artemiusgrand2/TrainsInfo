using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using TrainsInfo.Common.Enums;
using TrainsInfo.Common.BusinessObjects;
using TrainsInfo.Common.Interfaces;
using TrainsInfo.Common.Infrastructures;

namespace TrainsInfo.DataParser.NodeOT
{
    public class NodeOTDataParser : IDataParser
    {
        private readonly string OTTrain = "OT";
        private readonly IList<string> codesOperation = new List<string> { "Р0005" };

        public IList<RowValue> Parse(object data, IList<IInfrastructure> infrastructures)
        {
            var table = data as IList<ModelDataIAS_PYR_GP>;
            var result = new List<RowValue>();
            if (table != null)
            {
                var tableWithFilter = table.Where(x => codesOperation.Contains(x.OperationCode) && x.StationCode == x.Index1 && x.TrainNumber != "9999").ToList();
                foreach (var node in infrastructures.Where(x => x.Type == TypeInfrastructure.node).ToList())
                {
                    var countTrain = tableWithFilter.Where(x => (node as Node).ListStations.Contains(x.StationCode)).ToList();
                    result.Add(new RowValue(node.Station, OTTrain, countTrain.ToString()));
                }
            }
            //
            return result;
        }

    }
}
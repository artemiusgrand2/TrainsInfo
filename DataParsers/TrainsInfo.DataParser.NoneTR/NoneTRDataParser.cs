using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using TrainsInfo.Common.Enums;
using TrainsInfo.Common.BusinessObjects;
using TrainsInfo.Common.Interfaces;
using TrainsInfo.Common.Infrastructures;

namespace TrainsInfo.DataParser.NodeTR
{
    public class NodeTRDataParser : IDataParser
    {
        private readonly string TRTrain = "TR";
        private readonly IList<string> codesOperation = new List<string> { "P0001", "P0101", "P0201", "P0009", "P0074" };

        public IList<RowValue> Parse(object data, IList<InfrastructureBase> infrastructures)
        {
            var table = data as IList<ModelBase>;
            var result = new List<RowValue>();
            if (table != null)
            {
                var tableIAS_PYR_GP = table.Select(x => x as ModelDataIAS_PYR_GP).ToList();
                var tableWithFilter = tableIAS_PYR_GP.Where(x => codesOperation.Contains(x.OperationCode) && x.StationCode != x.Index3 && x.TrainNumber != 9999).ToList();
                foreach (var node in infrastructures.Where(x => x.Type == TypeInfrastructure.node).ToList())
                {
                    var events = tableWithFilter.Where(x => (node as Node).ListStations.Contains(x.StationCode) && !(node as Node).ListStations.Contains(x.Index3));
                    Logger.Log.LogInfo("Узел {0} транзит:", node.Station);
                    var index = 1;

                    foreach (var newEvent in events)
                    {
                        Logger.Log.LogInfo("{0}. {1}", index, new JavaScriptSerializer().Serialize(newEvent));
                        newEvent.IsApply = true;
                        index++;
                    }
                    //
                    var countTrain = events.Count().ToString();
                    result.Add(new RowValue(node.Station, TRTrain, (countTrain.Length < 2) ? countTrain.Insert(0, " ") : countTrain, DateTime.Now));

                }
            }
            //
            return result;
        }
    }
}

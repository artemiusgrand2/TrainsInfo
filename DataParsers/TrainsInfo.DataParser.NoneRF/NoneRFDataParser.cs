﻿using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using TrainsInfo.Common.Enums;
using TrainsInfo.Common.BusinessObjects;
using TrainsInfo.Common.Interfaces;
using TrainsInfo.Common.Infrastructures;

namespace TrainsInfo.DataParser.NodeRF
{
    public class NodeRFDataParser : IDataParser
    {
        private readonly string RFTrain = "RF";
        private readonly IList<string> codesOperation = new List<string> { "P0001", "P0011", "P0201" };

        public IList<RowValue> Parse(object data, IList<InfrastructureBase> infrastructures)
        {
            var table = data as IList<ModelDataIAS_PYR_GP>;
            var result = new List<RowValue>();
            if (table != null)
            {
                var tableWithFilter = table.Where(x => codesOperation.Contains(x.OperationCode) && x.StationCode == x.Index3 && x.TrainNumber != 9999).ToList();
                foreach (var node in infrastructures.Where(x => x.Type == TypeInfrastructure.node).ToList())
                {
                    var events = tableWithFilter.Where(x => (node as Node).ListStations.Contains(x.StationCode));
                    Logger.Log.LogInfo("Узел {0} расформирование:", node.Station);
                    var index = 1;

                    foreach (var newEvent in events)
                    {
                        Logger.Log.LogInfo("{0}. {1}", index, new JavaScriptSerializer().Serialize(newEvent));
                        index++;
                    }
                    var countTrain = events.Count().ToString();
                    result.Add(new RowValue(node.Station,  RFTrain, (countTrain.Length < 2) ? countTrain.Insert(0, " ") : countTrain, DateTime.Now));
                }
            }
            //
            return result;
        }

    }
}
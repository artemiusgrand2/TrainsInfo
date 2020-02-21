using System;
using System.Linq;
using System.Collections.Generic;
using TrainsInfo.Common.Enums;
using TrainsInfo.Configuration.Records;
using TrainsInfo.Common.Interfaces;

namespace TrainsInfo.Common.Infrastructures
{
    public class Area : Node
    {
        public int Station2 { get; }

        public IList<KeyValuePair<string, string>> Nodes { get; } = new List<KeyValuePair<string, string>>();

        public IList<string> Areas { get; } = new List<string>();

        public Area(int station1, int station2, IList<string> listStations, IList<NodeRecord> nodes, IList<string> areas) : base(station1, listStations)
        {
            Station2 = station2;
            Type = TypeInfrastructure.area;
            if (areas != null)
                Areas = areas;
            //
            if(nodes != null)
            {
                foreach (var node in nodes)
                {
                    if(Nodes.Where(x=>x.Key == node.Station && x.Value == node.StationDirection).Count() == 0)
                        Nodes.Add(new KeyValuePair<string, string>(node.Station, node.StationDirection));
                    //
                    if (Nodes.Where(x => x.Key == node.StationDirection && x.Value == node.Station).Count() == 0)
                        Nodes.Add(new KeyValuePair<string, string>(node.StationDirection, node.Station));
                }
            }

        }
    }
}


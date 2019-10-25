﻿using System;
using System.Collections.Generic;
using TrainsInfo.Common.Enums;
using TrainsInfo.Configuration.Records;
using TrainsInfo.Common.Interfaces;

namespace TrainsInfo.Common.Infrastructures
{
    public class Area : Node
    {
        public int Station2 { get; }

        public IDictionary<string, string> Nodes { get; } = new Dictionary<string, string>();

        public Area(int station1, int station2, IList<string> listStations, IList<NodeRecord> nodes) : base(station1, listStations)
        {
            Station2 = station2;
            Type = TypeInfrastructure.area;
            //
            foreach (var node in nodes)
            {
                if (!Nodes.ContainsKey(node.Station))
                    Nodes.Add(node.Station, node.StationDirection);
                //
                if (!Nodes.ContainsKey(node.StationDirection))
                    Nodes.Add(node.StationDirection, node.Station);
            }
        }
    }
}


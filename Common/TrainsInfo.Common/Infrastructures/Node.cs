using System;
using System.Collections.Generic;
using TrainsInfo.Common.Enums;
using TrainsInfo.Common.Interfaces;

namespace TrainsInfo.Common.Infrastructures
{
    public class Node : Joint
    {
        public IList<string> ListStations { get;  }

        public Node(int station, IList<string> listStations):base(station)
        {
            ListStations = listStations;
            Type = TypeInfrastructure.node;
        }
    }
}

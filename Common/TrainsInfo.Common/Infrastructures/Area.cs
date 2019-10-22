using System;
using System.Collections.Generic;
using TrainsInfo.Common.Enums;
using TrainsInfo.Common.Interfaces;

namespace TrainsInfo.Common.Infrastructures
{
    public class Area : Node
    {
        public int Station2 { get; }

        public Area(int station1, int station2, IList<string> listStations) : base(station1, listStations)
        {
            Station2 = station2;
            Type = TypeInfrastructure.area;
        }
    }
}


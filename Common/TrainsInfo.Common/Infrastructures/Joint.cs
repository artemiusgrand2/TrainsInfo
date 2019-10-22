using System;
using System.Collections.Generic;
using TrainsInfo.Common.Enums;
using TrainsInfo.Common.Interfaces;

namespace TrainsInfo.Common.Infrastructures
{
     public class Joint : IInfrastructure
    {
        public int Station { get; }

        public TypeInfrastructure Type { get; protected set; } 

        public Joint(int station)
        {
            Station = station;
            Type = TypeInfrastructure.joint;
        }
    }
}
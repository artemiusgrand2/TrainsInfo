using System;
using System.Collections.Generic;
using TrainsInfo.Common.Enums;
using TrainsInfo.Common.Interfaces;
using TrainsInfo.Common.BusinessObjects;

namespace TrainsInfo.Common.Infrastructures
{
    public class Joint : InfrastructureBase
    {
        public Joint(int station)
        {
            Station = station;
            Type = TypeInfrastructure.joint;
        }
    }
}
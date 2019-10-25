using System;
using System.Collections.Generic;
using TrainsInfo.Common.Enums;
using TrainsInfo.Common.Interfaces;

namespace TrainsInfo.Common.Infrastructures
{
    public class Table : InfrastructureBase
    {
        public ViewTable View { get; }

        public Table(TypeInfrastructure type, ViewTable view)
        {
            Type = TypeInfrastructure.table;
            View = view;
        }
    }
}

﻿using System;
using System.Collections.Generic;

namespace TrainsInfo.Common.BusinessObjects
{
    public class RowValue : BaseValue
    {
        public int Station1 { get;  }

        public int Station2 { get;  }

        public string Name { get;  }

        public IList<ModelTrainPassOpz> TrainsPassOpz { get; internal set; } = new List<ModelTrainPassOpz>();


        public RowValue(string name, string value, DateTime lastUpdate):base(value, lastUpdate)
        {
            Name = name;
        }
        public RowValue(string name, string value, DateTime lastUpdate, IList<ModelTrainPassOpz> trainsPassOpz) : base(value, lastUpdate)
        {
            Name = name;
            TrainsPassOpz = trainsPassOpz;
        }

        public RowValue(int station1, int station2, string name, string value, DateTime lastUpdate) :base(value, lastUpdate)
        {
            Station1 = station1;
            Station2 = station2;
            Name = name;
        }

        public RowValue(int station1, string name, string value, DateTime lastUpdate) : base(value, lastUpdate)
        {
            Station1 = station1;
            Station2 = 0;
            Name = name;
        }

    }
}

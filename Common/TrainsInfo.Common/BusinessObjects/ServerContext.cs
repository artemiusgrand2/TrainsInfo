using System;
using System.Collections.Generic;
using TrainsInfo.Common.Interfaces;

namespace TrainsInfo.Common.BusinessObjects
{
     public  class ServerContext
    {
        public string ConfigPath { get; set; }

        public IDictionary<string, DataSource> DataSources { get; private set; }
        public IList<IInfrastructure> Infrastructures { get; private set; }

        public DataStorage DataStorage { get; set; }


        public ServerContext()
        {
            DataSources = new Dictionary<string, DataSource>();
            Infrastructures = new List<IInfrastructure>();
        }

    }
}

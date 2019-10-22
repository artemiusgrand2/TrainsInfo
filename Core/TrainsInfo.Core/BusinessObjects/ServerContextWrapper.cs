using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrainsInfo.Common.BusinessObjects;

namespace TrainsInfo.Core.BusinessObjects
{
    public class ServerContextWrapper
    {
        private static readonly object locker = new object();
        private static readonly ServerContext instance;

        public static ServerContext Instance
        {
            get
            {
                return instance;
            }
        }

        static ServerContextWrapper()
        {
            if (instance == null)
            {
                lock (locker)
                {
                    if (instance == null)
                    {
                        instance = new ServerContext();
                    }
                }
            }
        }
    }
}

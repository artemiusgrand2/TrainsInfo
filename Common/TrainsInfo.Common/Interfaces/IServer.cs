using System;
using System.Collections.Generic;
using TrainsInfo.Common.Enums;

namespace TrainsInfo.Common.Interfaces
{
    public interface IServer
    {
        IDictionary<string, string> GetTables(CategotyTable categoty);
    }
}

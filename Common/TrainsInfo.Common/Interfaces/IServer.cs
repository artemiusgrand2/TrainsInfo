using System;
using System.Collections.Generic;
using TrainsInfo.Common.Enums;
using TrainsInfo.Common.BusinessObjects;

namespace TrainsInfo.Common.Interfaces
{
    public interface IServer
    {
        IDictionary<string, BaseValue> GetTables(CategotyTable categoty);
    }
}

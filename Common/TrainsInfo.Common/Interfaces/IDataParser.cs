using System.Collections.Generic;
using TrainsInfo.Common.BusinessObjects;
using TrainsInfo.Common.Infrastructures;

namespace TrainsInfo.Common.Interfaces
{
    public interface IDataParser
    {
        IList<RowValue> Parse(object data, IList<InfrastructureBase> infrastructures);

    }
}

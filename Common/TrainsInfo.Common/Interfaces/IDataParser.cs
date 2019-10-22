using System.Collections.Generic;
using TrainsInfo.Common.BusinessObjects;
using TrainsInfo.Common.Interfaces;

namespace TrainsInfo.Common.Interfaces
{
    public interface IDataParser
    {
        IList<RowValue> Parse(object data, IList<IInfrastructure> infrastructures);

    }
}

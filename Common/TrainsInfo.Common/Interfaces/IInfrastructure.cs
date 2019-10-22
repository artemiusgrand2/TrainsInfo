using TrainsInfo.Common.Enums;

namespace TrainsInfo.Common.Interfaces
{
    public interface IInfrastructure
    {
        int Station { get; }

        TypeInfrastructure Type { get;}
    }
}

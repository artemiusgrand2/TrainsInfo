using TrainsInfo.Configuration.Records;
using TrainsInfo.Common.Interfaces;
using TrainsInfo.Common.Enums;

namespace TrainsInfo.Common.Infrastructures
{
    public class InfrastructureFactory
    {

        public static IInfrastructure Create(InfrastructureRecord record)
        {
            if (record.Type == "Node")
            {
                return new Node(record.StationCode1, record.StationCodes);
            }
            else if (record.Type == "Area")
            {
                return new Area(record.StationCode1, record.StationCode2, record.StationCodes);
            }
            else if (record.Type == "Joint")
            {
                return new Joint(record.StationCode1);
            }
            //
            return null;
        }
    }
}

using TrainsInfo.Configuration.Records;
using TrainsInfo.Common.Interfaces;
using TrainsInfo.Common.Enums;

namespace TrainsInfo.Common.Infrastructures
{
    public class InfrastructureFactory
    {

        public static InfrastructureBase Create(InfrastructureRecord record)
        {
            if (record.Type == "Node")
            {
                return new Node(record.Station1, record.StationCodes);
            }
            else if (record.Type == "Area")
            {
                return new Area(record.Station1, record.Station2, record.StationCodes, record.Nodes);
            }
            else if (record.Type == "Joint")
            {
                return new Joint(record.Station1);
            }
            //
            return null;
        }
    }
}

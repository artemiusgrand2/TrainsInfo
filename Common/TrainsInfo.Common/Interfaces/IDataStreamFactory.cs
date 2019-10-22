using TrainsInfo.Configuration.Records;

namespace TrainsInfo.Common.Interfaces
{
    public interface IDataStreamFactory
    {
        IDataStream CreateClientStream(DataStreamRecord record);
    }
}

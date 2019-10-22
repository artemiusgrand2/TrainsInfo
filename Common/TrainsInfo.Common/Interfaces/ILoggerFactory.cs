using TrainsInfo.Configuration.Records;

namespace TrainsInfo.Common.Interfaces
{
    public interface ILoggerFactory
    {
        ILogger Create(LoggerRecord record);
    }
}

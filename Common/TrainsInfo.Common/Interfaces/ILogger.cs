

namespace TrainsInfo.Common.Interfaces
{
    public interface ILogger
    {
        void ObligatoryInfo(string format, params object[] args);
        void LogInfo(string format, params object[] args);
        void LogWarn(string format, params object[] args);
        void LogError(string format, params object[] args);
        void LogDebug(string format, params object[] args);
        //второй файл лога
        void OthersTrainsInfo(string format, params object[] args);
        //третий файл лога
        void AreaSubTrainsInfo(string format, params object[] args);
    }
}

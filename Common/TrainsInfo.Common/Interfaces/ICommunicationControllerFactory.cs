using TrainsInfo.Configuration.Records;

namespace TrainsInfo.Common.Interfaces
{
    public interface ICommunicationControllerFactory
    {
        ICommunicationController Create(SettingableRecord settings, IDataStream client, IServer server);
    }
}

using TrainsInfo.DataStream.Communication.Protocol.Dsccp.Enums;

namespace TrainsInfo.DataStream.Communication.Protocol.Dsccp
{
    public abstract class Message
    {
        public abstract MessageType MType { get; }
    }
}

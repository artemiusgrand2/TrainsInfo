using TrainsInfo.DataStream.Communication.Protocol.Dsccp.Enums;

namespace TrainsInfo.DataStream.Communication.Protocol.Dsccp.Requests
{
    public abstract class Request : Message
    {
        public abstract RequestType RType { get; }

        public override MessageType MType
        {
            get { return MessageType.Request; }
        }
    }
}

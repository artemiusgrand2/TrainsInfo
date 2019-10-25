using System;

namespace TrainsInfo.DataStream.Communication.Protocol.Dsccp.Exceptions
{
    public class InvalidMessageTypeException : Exception
    {
        public int MessageType { get; private set; }

        public InvalidMessageTypeException(int messageType)
        {
            MessageType = messageType;
        }
    }
}

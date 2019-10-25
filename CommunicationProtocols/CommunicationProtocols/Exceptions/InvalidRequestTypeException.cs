using System;

namespace TrainsInfo.DataStream.Communication.Protocol.Dsccp.Exceptions
{
    public class InvalidRequestTypeException : Exception
    {
        public int RequestType { get; private set; }

        public InvalidRequestTypeException(int requestType)
        {
            RequestType = requestType;
        }
    }
}

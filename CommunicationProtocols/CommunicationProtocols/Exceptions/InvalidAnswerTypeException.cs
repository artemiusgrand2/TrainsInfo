using System;

namespace TrainsInfo.DataStream.Communication.Protocol.Dsccp.Exceptions
{
    public class InvalidAnswerTypeException : Exception
    {
        public int AnswerType { get; private set; }

        public InvalidAnswerTypeException(int answerType)
        {
            AnswerType = answerType;
        }
    }
}

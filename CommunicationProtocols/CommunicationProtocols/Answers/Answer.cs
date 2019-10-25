using TrainsInfo.DataStream.Communication.Protocol.Dsccp.Enums;

namespace TrainsInfo.DataStream.Communication.Protocol.Dsccp.Answers
{
    public abstract class Answer : Message
    {
        public abstract AnswerType AType { get; }

        public override MessageType MType
        {
            get { return MessageType.Answer; }
        }

    }
}

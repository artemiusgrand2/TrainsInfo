using System;
using System.Collections.Generic;
using TrainsInfo.DataStream.Communication.Protocol.Dsccp.Answers;
using TrainsInfo.DataStream.Communication.Protocol.Dsccp.Enums;
using TrainsInfo.DataStream.Communication.Protocol.Dsccp.Interfaces;
using TrainsInfo.DataStream.Communication.Protocol.Dsccp.Exceptions;

namespace TrainsInfo.DataStream.Communication.Protocol.Dsccp.Converters
{
    internal class AnswerConverter : IConverter
    {
        private readonly IDictionary<AnswerType, IConverter> converters = new Dictionary<AnswerType, IConverter>()
            {
                {
                    AnswerType.TypeTable, new TypeTableAnswerConverter()
                }
            };

        public Message FromBytes(object data)
        {
            return null;
        }

        public byte[] ToBytes(Message message)
        {
            byte[] result;
            Answer answer = message as Answer;
            if (answer != null)
            {
                IConverter converter;
                if (converters.TryGetValue(answer.AType, out converter))
                {
                    result = converter.ToBytes(answer);
                }
                else
                {
                    throw new InvalidAnswerTypeException((int)answer.AType);
                }
            }
            else
            {
                throw new InvalidMessageTypeException((int)message.MType);
            }

            return result;
        }
    }
}

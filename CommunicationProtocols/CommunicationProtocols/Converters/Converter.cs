using System.Collections.Generic;
using System.Collections.Specialized;
using TrainsInfo.DataStream.Communication.Protocol.Dsccp.Exceptions;
using TrainsInfo.DataStream.Communication.Protocol.Dsccp.Enums;
using TrainsInfo.DataStream.Communication.Protocol.Dsccp.Interfaces;

namespace TrainsInfo.DataStream.Communication.Protocol.Dsccp.Converters
{
    public class Converter
    {
        private const int HeaderSize = 9;

        private readonly IDictionary<MessageType, IConverter> converters = new Dictionary<MessageType, IConverter>()
            {
                { MessageType.Answer, new AnswerConverter()},
                { MessageType.Request, new RequestConverter()},
             //   { MessageType.Error, new ReConverter()},
            };

        protected Converter()
        {
        }

        public Message FromBytes(object data)
        {
            var parser = (NameValueCollection)data;
            if (parser != null)
            {
                IConverter converter;
                if (converters.TryGetValue(MessageType.Request, out converter))
                {
                    return converter.FromBytes(data);
                }
            }
            return null;
        }

        public byte[] ToBytes(Message message)
        {
            IConverter converter;
            if (converters.TryGetValue(message.MType, out converter))
            {
                return converter.ToBytes(message);
            }
            else
            {
                throw new InvalidMessageTypeException((int)message.MType);
            }
        }

        public static Converter CreateInstance()
        {
            return new Converter();
        }
    }
}

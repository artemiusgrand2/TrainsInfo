using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Specialized;
using TrainsInfo.DataStream.Communication.Protocol.Dsccp.Answers;
using TrainsInfo.DataStream.Communication.Protocol.Dsccp.Enums;
using TrainsInfo.DataStream.Communication.Protocol.Dsccp.Interfaces;
using TrainsInfo.DataStream.Communication.Protocol.Dsccp.Exceptions;

namespace TrainsInfo.DataStream.Communication.Protocol.Dsccp.Converters
{
    internal class RequestConverter : IConverter
    {
        private readonly IDictionary<RequestType, IConverter> converters = new Dictionary<RequestType, IConverter>()
            {
                {
                    RequestType.TypeTable, new TypeTableRequestConverter()
                }
            };

        private readonly string paramTypeTable = "typeTable";

        public Message FromBytes(object data)
        {
            Message result;
            var parser = (NameValueCollection)data;
            if (parser.AllKeys.Contains(paramTypeTable))
            {
                IConverter converter;
                if (converters.TryGetValue(RequestType.TypeTable, out converter))
                {
                   return converter.FromBytes(parser[paramTypeTable]);
                }
            }
            //
            return null;
        }

        public byte[] ToBytes(Message message)
        {
            return null;
        }
    }
}

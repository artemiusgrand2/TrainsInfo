//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace TrainsInfo.DataStream.Communication.Protocol.Dsccp.Converters
//{
//    internal class ErrorConverter : IConverter
//    {
//        private readonly IDictionary<RequestType, IConverter> converters = new Dictionary<RequestType, IConverter>()
//            {
//                {
//                    RequestType.TypeTable, new TypeTableRequestConverter()
//                }
//            };

//        private readonly string paramTypeTable = "typeTable";

//        public Message FromBytes(object data)
//        {
//            Message result;
//            var parser = (NameValueCollection)data;
//            if (parser.AllKeys.Contains(paramTypeTable))
//            {
//                IConverter converter;
//                if (converters.TryGetValue(RequestType.TypeTable, out converter))
//                {
//                    result = converter.FromBytes(parser[paramTypeTable]);
//                }
//            }
//            //
//            return null;
//        }

//        public byte[] ToBytes(Message message)
//        {
//            return null;
//        }
//    }
//}

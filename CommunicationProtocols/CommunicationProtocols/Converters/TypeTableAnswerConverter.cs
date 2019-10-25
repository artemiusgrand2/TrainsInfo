using System;
using System.Text;
using System.Web.Script.Serialization;
using TrainsInfo.DataStream.Communication.Protocol.Dsccp.Interfaces;
using TrainsInfo.DataStream.Communication.Protocol.Dsccp.Answers;
using TrainsInfo.DataStream.Communication.Protocol.Dsccp.Exceptions;

namespace TrainsInfo.DataStream.Communication.Protocol.Dsccp.Converters
{
    internal class TypeTableAnswerConverter : IConverter
    {

        public Message FromBytes(object data)
        {
            return null;
        }

        public byte[] ToBytes(Message message)
        {
            var answer = message as TableTypeAnswer;
            if (answer != null)
            {
                return System.Text.Encoding.UTF8.GetBytes(new JavaScriptSerializer().Serialize(new { Tables = answer.Tables }));
            }
            throw new InvalidAnswerTypeException((byte)((Answer)message).AType);
        }
    }
}

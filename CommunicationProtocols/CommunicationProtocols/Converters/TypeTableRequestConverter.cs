using System;
using System.Collections.Generic;
using TrainsInfo.Common.Enums;
using TrainsInfo.DataStream.Communication.Protocol.Dsccp.Interfaces;
using TrainsInfo.DataStream.Communication.Protocol.Dsccp.Requests;
using TrainsInfo.DataStream.Communication.Protocol.Dsccp.Exceptions;

namespace TrainsInfo.DataStream.Communication.Protocol.Dsccp.Converters
{
    internal class TypeTableRequestConverter : IConverter
    {
        private readonly string paramTableLeft = "L";

        private readonly string paramTableRight = "R";

        public Message FromBytes(object data)
        {
            var request = new TableTypeRequest();

            if (data.ToString() == paramTableLeft)
                request.Categoty = CategotyTable.left;
            else
                request.Categoty = CategotyTable.right;
            //
            return request;
        }

        public byte[] ToBytes(Message message)
        {
            return null;
        }
    }
}

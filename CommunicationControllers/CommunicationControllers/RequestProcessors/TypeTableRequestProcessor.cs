using System;
using System.Collections.Generic;
using TrainsInfo.Common.Interfaces;
using TrainsInfo.DataStream.Communication.Controller.Dsccp.Interfaces;
using TrainsInfo.DataStream.Communication.Protocol.Dsccp.Answers;
using TrainsInfo.DataStream.Communication.Protocol.Dsccp.Requests;

namespace TrainsInfo.DataStream.Communication.Controller.Dsccp.RequestProcessors
{
    internal class TypeTableRequestProcessor : IRequestProcessor
    {
        public Answer Process(Request request, IServer server)
        {
            var answer = new TableTypeAnswer();
            answer.Tables = server.GetTables((request as TableTypeRequest).Categoty);

            return answer;
        }
    }
}

using System;
using System.Collections.Generic;
using TrainsInfo.DataStream.Communication.Protocol.Dsccp.Enums;
using TrainsInfo.Common.Enums;

namespace TrainsInfo.DataStream.Communication.Protocol.Dsccp.Answers
{
    public class TableTypeAnswer : Answer
    {
        public override AnswerType AType
        {
            get { return AnswerType.TypeTable; }
        }

        public IDictionary<string, string> Tables { get;  set; }

    }
}

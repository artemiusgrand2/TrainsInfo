using TrainsInfo.Common.Enums;
using TrainsInfo.DataStream.Communication.Protocol.Dsccp.Enums;

namespace TrainsInfo.DataStream.Communication.Protocol.Dsccp.Requests
{
    public class TableTypeRequest : Request
    {
        public override RequestType RType
        {
            get { return RequestType.TypeTable; }
        }

        public CategotyTable Categoty { get; set; }
    }
}

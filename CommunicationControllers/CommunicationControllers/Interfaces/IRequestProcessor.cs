using TrainsInfo.DataStream.Communication.Protocol.Dsccp.Requests;
using TrainsInfo.DataStream.Communication.Protocol.Dsccp.Answers;
using TrainsInfo.Common.Interfaces;

namespace TrainsInfo.DataStream.Communication.Controller.Dsccp.Interfaces
{
    internal interface IRequestProcessor
    {
        Answer Process(Request request, IServer server);
    }
}

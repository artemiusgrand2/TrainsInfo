using TrainsInfo.Common.Interfaces;
using TrainsInfo.Common.Attributes;

namespace TrainsInfo.DataParser.NodeOT
{
    [PluginType("NodeOT")]
    public class NodeOTDataParserFactory : IDataParserFactory
    {
        public IDataParser Create()
        {
            return new NodeOTDataParser();
        }
    }
}

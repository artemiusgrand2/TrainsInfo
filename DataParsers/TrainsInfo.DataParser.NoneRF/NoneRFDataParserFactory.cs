using TrainsInfo.Common.Interfaces;
using TrainsInfo.Common.Attributes;

namespace TrainsInfo.DataParser.NodeRF
{
    [PluginType("NodeRF")]
    public class NodeRFDataParserFactory : IDataParserFactory
    {
        public IDataParser Create()
        {
            return new NodeRFDataParser();
        }
    }
}
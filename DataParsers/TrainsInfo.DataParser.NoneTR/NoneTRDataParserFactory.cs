using TrainsInfo.Common.Interfaces;
using TrainsInfo.Common.Attributes;

namespace TrainsInfo.DataParser.NodeTR
{
    [PluginType("NodeTR")]
    public class NodeTRDataParserFactory : IDataParserFactory
    {
        public IDataParser Create()
        {
            return new NodeTRDataParser();
        }
    }
}
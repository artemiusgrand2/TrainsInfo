using TrainsInfo.Common.Interfaces;
using TrainsInfo.Common.Attributes;

namespace TrainsInfo.DataParser.REG_TAB_PLAN
{
    [PluginType("REG_TAB_PLAN")]
    public class REG_TAB_PLANDataParserFactory : IDataParserFactory
    {
        public IDataParser Create()
        {
            return new REG_TAB_PLANDataParser();
        }
    }
}
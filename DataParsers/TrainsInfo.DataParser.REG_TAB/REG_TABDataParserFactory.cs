using TrainsInfo.Common.Interfaces;
using TrainsInfo.Common.Attributes;

namespace TrainsInfo.DataParser.REG_TAB
{
    [PluginType("REG_TAB")]
    public class REG_TABDataParserFactory : IDataParserFactory
    {
        public IDataParser Create()
        {
            return new REG_TABDataParser();
        }
    }
}

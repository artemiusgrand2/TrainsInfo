using TrainsInfo.Common.Interfaces;
using TrainsInfo.Common.Attributes;

namespace TrainsInfo.DataParser.AreaGargo
{
    [PluginType("AreaGargo")]
    public class AreaGargoDaraParserFactory : IDataParserFactory
    {
        public IDataParser Create()
        {
            return new AreaGargoDaraParser();
        }
    }
}

using TrainsInfo.Common.Interfaces;
using TrainsInfo.Common.Attributes;

namespace TrainsInfo.DataParser.AreaPass
{
    [PluginType("AreaPass")]
    public class AreaPassDaraParserFactory : IDataParserFactory
    {
        public IDataParser Create()
        {
            return new AreaPassDaraParser();
        }
    }
}
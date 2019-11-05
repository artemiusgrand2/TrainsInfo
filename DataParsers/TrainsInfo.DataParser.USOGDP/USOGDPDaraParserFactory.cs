using TrainsInfo.Common.Interfaces;
using TrainsInfo.Common.Attributes;

namespace TrainsInfo.DataParser.USOGDP
{
    [PluginType("USOGDP")]
    public class USOGDPDaraParserFactory : IDataParserFactory
    {
        public IDataParser Create()
        {
            return new USOGDPDaraParser();
        }
    }
}
using TrainsInfo.Common.Interfaces;
using TrainsInfo.Common.Attributes;

namespace TrainsInfo.DataParser.Vostok_Zapad
{
    [PluginType("Vostok_Zapad")]
    public  class Vostok_ZapadDataParserFactory : IDataParserFactory
    {
        public IDataParser Create()
        {
            return new Vostok_ZapadDataParser();
        }
    }
}


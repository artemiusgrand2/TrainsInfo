using TrainsInfo.Common.Interfaces;
using TrainsInfo.Common.Attributes;

namespace TrainsInfo.DataParser.Zapad_Vostok
{
    [PluginType("Zapad_Vostok")]
    public class Zapad_VostokDataParserFactory : IDataParserFactory
    {
        public IDataParser Create()
        {
            return new Zapad_VostokDataParser();
        }
    }
}

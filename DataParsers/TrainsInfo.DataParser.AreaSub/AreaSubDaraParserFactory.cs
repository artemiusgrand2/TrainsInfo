using TrainsInfo.Common.Interfaces;
using TrainsInfo.Common.Attributes;

namespace TrainsInfo.DataParser.AreaSub
{
    [PluginType("AreaSub")]
    public class AreaSubDaraParserFactory : IDataParserFactory
    {
        public IDataParser Create()
        {
            return new AreaSubDaraParser();
        }
    }
}

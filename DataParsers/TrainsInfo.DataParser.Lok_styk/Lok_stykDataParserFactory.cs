using TrainsInfo.Common.Interfaces;
using TrainsInfo.Common.Attributes;

namespace TrainsInfo.DataParser.Lok_styk
{
    [PluginType("Lok_styk")]
    public class Lok_stykDataParserFactory : IDataParserFactory
    {
        public IDataParser Create()
        {
            return new Lok_stykDataParser();
        }
    }
}
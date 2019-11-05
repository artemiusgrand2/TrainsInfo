using TrainsInfo.Common.Interfaces;
using TrainsInfo.Common.Attributes;
namespace TrainsInfo.DataParser.Lok_ojto2
{
    [PluginType("Lok_ojto2")]
    public class Lok_ojto2DataParserFactory : IDataParserFactory
    {
        public IDataParser Create()
        {
            return new Lok_ojto2DataParser();
        }
    }
}
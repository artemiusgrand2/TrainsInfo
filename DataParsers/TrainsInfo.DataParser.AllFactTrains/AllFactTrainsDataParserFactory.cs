using TrainsInfo.Common.Interfaces;
using TrainsInfo.Common.Attributes;

namespace TrainsInfo.DataParser.AllFactTrains
{
    [PluginType("AllFactTrains")]
    public class AllFactTrainsDataParserFactory : IDataParserFactory
    {
        public IDataParser Create()
        {
            return new AllFactTrainsDataParser();
        }
    }
}

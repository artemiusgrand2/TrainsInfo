using TrainsInfo.Common.Interfaces;
using TrainsInfo.Common.Attributes;

namespace TrainsInfo.DataParser.Lok_nod
{
    [PluginType("Lok_nod")]
    public class Lok_nodDataParserFactory : IDataParserFactory
    {
        public IDataParser Create()
        {
            return new Lok_nodDataParser();
        }
    }
}

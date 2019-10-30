using TrainsInfo.Common.Interfaces;
using TrainsInfo.Common.Attributes;

namespace TrainsInfo.DataParser.BroshPoezd
{
    [PluginType("BroshPoezd")]
    public class BroshPoezdDataParserFactory : IDataParserFactory
    {
        public IDataParser Create()
        {
            return new BroshPoezdDataParser();
        }
    }
}

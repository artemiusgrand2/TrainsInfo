using TrainsInfo.Common.Interfaces;
using TrainsInfo.Common.Attributes;

namespace TrainsInfo.DataParser.Razvoz
{
    [PluginType("Razvoz")]
    public class RazvozDataParserFactory : IDataParserFactory
    {
        public IDataParser Create()
        {
            return new RazvozDataParser();
        }
    }
}

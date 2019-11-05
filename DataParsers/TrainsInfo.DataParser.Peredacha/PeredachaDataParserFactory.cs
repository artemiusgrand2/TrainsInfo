using TrainsInfo.Common.Interfaces;
using TrainsInfo.Common.Attributes;
namespace TrainsInfo.DataParser.Peredacha
{
    [PluginType("Peredacha")]
    public class PeredachaDataParserFactory : IDataParserFactory
    {
        public IDataParser Create()
        {
            return new PeredachaDataParser();
        }
    }
}
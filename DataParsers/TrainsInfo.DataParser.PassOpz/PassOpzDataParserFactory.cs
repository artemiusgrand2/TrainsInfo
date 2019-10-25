using TrainsInfo.Common.Interfaces;
using TrainsInfo.Common.Attributes;
namespace TrainsInfo.DataParser.PassOpz
{
    [PluginType("PassOpz")]
    public class PassOpzDataParserFactory : IDataParserFactory
    {
        public IDataParser Create()
        {
            return new PassOpzDataParser();
        }
    }
}


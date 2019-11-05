using TrainsInfo.Common.Interfaces;
using TrainsInfo.Common.Attributes;
namespace TrainsInfo.DataParser.GruzRabota
{
    [PluginType("GruzRabota")]
    public class GruzRabotaDataParserFactory : IDataParserFactory
    {
        public IDataParser Create()
        {
            return new GruzRabotaDataParser();
        }
    }
}
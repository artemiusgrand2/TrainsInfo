using TrainsInfo.Common.Interfaces;
using TrainsInfo.Common.Attributes;

namespace TrainsInfo.DataParser.JointAllVagon
{
    [PluginType("JointAllVagon")]
    public class JointAllVagonDataParserFactory : IDataParserFactory
    {
        public IDataParser Create()
        {
           return new JointAllVagonDataParser();
        }
    }
}

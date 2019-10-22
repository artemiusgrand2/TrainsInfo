using System;

namespace TrainsInfo.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PluginTypeAttribute : Attribute
    {
        private readonly string pluginType;

        public string PluginType
        {
            get
            {
                return pluginType;
            }
        }

        public PluginTypeAttribute(string pluginType)
        {
            this.pluginType = pluginType;
        }
    }
}

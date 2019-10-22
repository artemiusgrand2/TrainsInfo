using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using TrainsInfo.Common.Attributes;

namespace TrainsInfo.Common.BusinessObjects
{
    public class PluginManager<T> where T : class
    {
        private readonly IDictionary<string, T> plugins;

        public IDictionary<string, T> Plugins
        {
            get
            
            {
                return plugins;
            }
        }
 
        public PluginManager()
        {
            plugins = new Dictionary<string, T>();
        }

        public void Load(string path, string filter)
        {
            string[] files = Directory.GetFiles(path, filter);
            foreach (string file in files)
            {
                Assembly assembly = Assembly.LoadFile(Path.GetFullPath(file));
                Type[] types = assembly.GetTypes();
                foreach (Type type in types)
                {
                    if (type.IsClass && typeof(T).IsAssignableFrom(type))
                    {
                        T plugin = Activator.CreateInstance(type) as T;
                        if (plugin != null)
                        {
                            string pluginType;
                            if (GetPluginType(plugin, out pluginType))
                            {
                                plugins.Add(pluginType, plugin);
                            }
                        }
                    }
                }
            }
        }

        private static bool GetPluginType(T plugin, out string type)
        {
            type = null;

            object[] attributes = plugin.GetType().GetCustomAttributes(true);

            for (int i = 0; i < attributes.Length; i++)
            {
                if (attributes[i] is PluginTypeAttribute)
                {
                    type = ((PluginTypeAttribute)attributes[i]).PluginType;
                    return true;
                }
            }
            return false;
        }
    }
}

using System;
using System.Xml.Serialization;

namespace TrainsInfo.Configuration.Records
{
    [Serializable]
    public abstract class SettingableRecord
    {
        [XmlArray("Settings")]
        [XmlArrayItem("Setting")]
        public SettingRecord[] Settings { get; set; }

        [XmlIgnore]
        public string this[string key]
        {
            get
            {
                foreach (SettingRecord settingRecord in Settings)
                {
                    if (settingRecord.Key == key)
                    {
                        return settingRecord.Value;
                    }
                }
                throw new ArgumentException("Key {0} has not been found", key);
            }
        }

        public bool TryGetSetting(string key, out string value)
        {
            value = null;
            if (Settings == null)
            {
                return false;
            }

            foreach (SettingRecord settingRecord in Settings)
            {
                if (settingRecord.Key == key)
                {
                    value = settingRecord.Value;
                    return true;
                }
            }

            return false;
        }

        public abstract string GetDiagnosticsInfo();
    }
}

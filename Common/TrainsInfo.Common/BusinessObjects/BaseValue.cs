using System;

namespace TrainsInfo.Common.BusinessObjects
{
    public class BaseValue
    {
        public string Value { get; internal set; }

        public DateTime LastUpdate { get; internal set; }

        public string Delta
        {
            get
            {
                var delta = (DateTime.Now >= LastUpdate) ? (DateTime.Now - LastUpdate) : new TimeSpan(0, 0, 0, 0, 0);
                return string.Format("{0:D2}:{1:D2}", (delta.Days > 0) ? (delta.Hours + delta.Days * 24) : delta.Hours, delta.Minutes);
            }
        }

        public BaseValue(string value, DateTime lastUpdate)
        {
            Value = value;
            LastUpdate = lastUpdate;
        }
    }
}

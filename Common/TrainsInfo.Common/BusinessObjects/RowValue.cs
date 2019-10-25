
namespace TrainsInfo.Common.BusinessObjects
{
    public class RowValue
    {
        public int Station1 { get;  }

        public int Station2 { get;  }

        public string Name { get;  }

        public string Value { get; internal set; }

        public RowValue(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public RowValue(int station1, int station2, string name, string value)
        {
            Station1 = station1;
            Station2 = station2;
            Name = name;
            Value = value;
        }

        public RowValue(int station1,  string name, string value)
        {
            Station1 = station1;
            Station2 = 0;
            Name = name;
            Value = value;
        }
    }
}

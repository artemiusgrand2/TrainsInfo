using System.Collections.Generic;
using System.Linq;
using TrainsInfo.Common.BusinessObjects;
using TrainsInfo.Common.Enums;
using TrainsInfo.Common.Interfaces;


namespace TrainsInfo.Common.Infrastructures
{
    public abstract class InfrastructureBase
    {
        public int Station { get; protected set; }

        public TypeInfrastructure Type { get; protected set; }

        //public void ProcessSourceData(IList<RowValue> data)
        //{
        //    lock (values)
        //    {
        //        var newValues = new List<RowValue>();
        //        foreach (var newValue in data)
        //        {
        //            var find = values.Where(x => x.Station1 == newValue.Station1 && x.Station2 == newValue.Station2 && x.Name == newValue.Name).FirstOrDefault();
        //            if (find != null)
        //            {
        //                if (newValue.Value != find.Value)
        //                {
        //                    newValues.Add(newValue);
        //                    find.Value = newValue.Value;
        //                }
        //            }
        //            else
        //            {
        //                values.Add(newValue);
        //                newValues.Add(newValue);
        //            }
        //        }
        //        //
        //        if (newValues.Count > 0)
        //            OnNewValues(newValues);
        //    }
        //}
    }
}

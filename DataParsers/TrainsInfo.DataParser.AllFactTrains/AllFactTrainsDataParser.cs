using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using TrainsInfo.Common.Enums;
using TrainsInfo.Common.BusinessObjects;
using TrainsInfo.Common.Interfaces;
using TrainsInfo.Common.Infrastructures;

namespace TrainsInfo.DataParser.AllFactTrains
{
    public class AllFactTrainsDataParser : IDataParser
    {
        private readonly string rowInPattern = @"\s*'IN'\s*;\s*'([0-9]+)'\s*;\s*'([0-9]+)'\s*;\s*'([0-9]+)'\s*;\s*'([0-9]+)'\s*;\s*'([0-9]+)'\s*";
        private readonly string rowOutPattern = @"\s*'OUT'\s*;\s*'([0-9]+)'\s*;\s*'([0-9]+)'\s*;\s*'([0-9]+)'\s*;\s*'([0-9]+)'\s*;\s*'([0-9]+)'\s*";

        private readonly string FactDeliveryTrains = "FS_TR";
        private readonly string FactDeliveryVagons = "FS_VG";
        private readonly string FactReceptionTrains = "FP_TR";
        private readonly string FactReceptionVagons = "FP_VG";


        private readonly string PlanDeliveryTrains = "PP_TR";
        private readonly string PlanDeliveryVagons = "PP_VG";

        private readonly string PlanReceptionTrains = "PS_TR";
        private readonly string PlanReceptionVagons = "PS_VG";


        public IList<RowValue> Parse(object data, IList<InfrastructureBase> infrastructures)
        {
            var table = (BaseValue)data;
            var result = new List<RowValue>();
            if (table != null)
            {
                if (infrastructures != null && infrastructures.Count > 0)
                {
                    var Joints = infrastructures.Where(x => x.Type == TypeInfrastructure.joint).ToList();
                    foreach (var record in table.Value.Split(new string[] { Environment.NewLine},  StringSplitOptions.None))
                    {
                        var rowMatch = Regex.Match(record, rowInPattern);
                        if (rowMatch.Success)
                            result.AddRange(GetRowValues(rowMatch, true, Joints));
                        else if ((rowMatch = Regex.Match(record, rowOutPattern)).Success)
                            result.AddRange(GetRowValues(rowMatch, false, Joints));
                    }
                    //
                    Joints.Where(x => result.Where(y => y.Station1 == x.Station && y.Name == FactDeliveryTrains).FirstOrDefault() == null).ToList().ForEach(x =>
                    {
                        result.Add(new RowValue(x.Station, FactDeliveryTrains, "0", DateTime.Now));
                    });
                    //
                    Joints.Where(x => result.Where(y => y.Station1 == x.Station && y.Name == FactDeliveryVagons).FirstOrDefault() == null).ToList().ForEach(x =>
                    {
                        result.Add(new RowValue(x.Station, FactDeliveryVagons, "0", DateTime.Now));
                    });
                    //
                    Joints.Where(x => result.Where(y => y.Station1 == x.Station && y.Name == FactReceptionTrains).FirstOrDefault() == null).ToList().ForEach(x =>
                    {
                        result.Add(new RowValue(x.Station, FactReceptionTrains, "0", DateTime.Now));
                    });
                    //
                    Joints.Where(x => result.Where(y => y.Station1 == x.Station && y.Name == FactReceptionVagons).FirstOrDefault() == null).ToList().ForEach(x =>
                    {
                        result.Add(new RowValue(x.Station, FactReceptionVagons, "0", DateTime.Now));
                    });
                    //
                    //
                    Joints.Where(x => result.Where(y => y.Station1 == x.Station && y.Name == PlanDeliveryTrains).FirstOrDefault() == null).ToList().ForEach(x =>
                    {
                        result.Add(new RowValue(x.Station, PlanDeliveryTrains, "0", DateTime.Now));
                    });
                    //
                    Joints.Where(x => result.Where(y => y.Station1 == x.Station && y.Name == PlanDeliveryVagons).FirstOrDefault() == null).ToList().ForEach(x =>
                    {
                        result.Add(new RowValue(x.Station, PlanDeliveryVagons, "0", DateTime.Now));
                    });
                    //
                    Joints.Where(x => result.Where(y => y.Station1 == x.Station && y.Name == PlanReceptionTrains).FirstOrDefault() == null).ToList().ForEach(x =>
                    {
                        result.Add(new RowValue(x.Station, PlanReceptionTrains, "0", DateTime.Now));
                    });
                    //
                    Joints.Where(x => result.Where(y => y.Station1 == x.Station && y.Name == PlanReceptionVagons).FirstOrDefault() == null).ToList().ForEach(x =>
                    {
                        result.Add(new RowValue(x.Station, PlanReceptionVagons, "0", DateTime.Now));
                    });
                }
            }
            //
            return result;
        }

        private IList<RowValue> GetRowValues(Match rowMatch, bool isInOperation, IList<InfrastructureBase> infrastructures)
        {
            var result = new List<RowValue>();
            var station1 = int.Parse(rowMatch.Groups[1].Value);
            var findInfrastructure = infrastructures.Where(x =>x.Station == station1).FirstOrDefault();
            if (findInfrastructure != null)
            {
                if (isInOperation)
                {
                    result.Add(new RowValue(station1, FactReceptionTrains, rowMatch.Groups[2].Value, DateTime.Now));
                    result.Add(new RowValue(station1, FactReceptionVagons, rowMatch.Groups[3].Value, DateTime.Now));
                    //
                    result.Add(new RowValue(station1, PlanDeliveryTrains, rowMatch.Groups[4].Value, DateTime.Now));
                    result.Add(new RowValue(station1, PlanDeliveryVagons, rowMatch.Groups[5].Value, DateTime.Now));
                }
                else
                {
                    result.Add(new RowValue(station1, FactDeliveryTrains, rowMatch.Groups[2].Value, DateTime.Now));
                    result.Add(new RowValue(station1, FactDeliveryVagons, rowMatch.Groups[3].Value, DateTime.Now));
                    //
                    result.Add(new RowValue(station1, PlanReceptionTrains, rowMatch.Groups[4].Value, DateTime.Now));
                    result.Add(new RowValue(station1, PlanReceptionVagons, rowMatch.Groups[5].Value, DateTime.Now));
                }
            }
            //
            return result;
        }
    }
}

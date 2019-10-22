using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using TrainsInfo.Common.Enums;
using TrainsInfo.Common.BusinessObjects;
using TrainsInfo.Common.Interfaces;

namespace TrainsInfo.DataParser.AllFactTrains
{
    public class AllFactTrainsDataParser : IDataParser
    {
        private readonly string rowInPattern = @"\s*'IN'\s*;\s*'([0-9]+)'\s*;\s*'([0-9]+)'\s*;\s*'([0-9]+)'\s*$";
        private readonly string rowOutPattern = @"\s*'OUT'\s*;\s*'([0-9]+)'\s*;\s*'([0-9]+)'\s*;\s*'([0-9]+)'\s*$";

        private readonly string FactDeliveryTrains = "FS_TR";
        private readonly string FactDeliveryVagons = "FS_VG";
        private readonly string FactReceptionTrains = "FP_TR";
        private readonly string FactReceptionVagons = "FP_VG";

        public IList<RowValue> Parse(object data, IList<IInfrastructure> infrastructures)
        {
            var table = data as string[];
            var result = new List<RowValue>();
            if (table != null)
            {
                if (infrastructures != null && infrastructures.Count > 0)
                {
                    foreach (var record in table)
                    {
                        var rowMatch = Regex.Match(record, rowInPattern);
                        if (rowMatch.Success)
                            result.AddRange(GetRowValues(rowMatch, true, infrastructures));
                        else if ((rowMatch = Regex.Match(record, rowOutPattern)).Success)
                            result.AddRange(GetRowValues(rowMatch, false, infrastructures));
                    }
                }
            }
            //
            return result;
        }

        private IList<RowValue> GetRowValues(Match rowMatch, bool isInOperation, IList<IInfrastructure> infrastructures)
        {
            var result = new List<RowValue>();
            var station1 = int.Parse(rowMatch.Groups[1].Value);
            var findInfrastructure = infrastructures.Where(x => x.Type == TypeInfrastructure.joint && x.Station == station1).FirstOrDefault();
            if (findInfrastructure != null)
            {
                result.Add(new RowValue(station1, (isInOperation) ? FactDeliveryTrains : FactReceptionTrains, rowMatch.Groups[2].Value));
                result.Add(new RowValue(station1, (isInOperation) ? FactDeliveryVagons : FactReceptionVagons, rowMatch.Groups[3].Value));
            }
            //
            return result;
        }
    }
}

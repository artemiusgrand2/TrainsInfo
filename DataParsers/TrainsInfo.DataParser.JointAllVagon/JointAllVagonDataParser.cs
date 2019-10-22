using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using TrainsInfo.Common.Enums;
using TrainsInfo.Common.BusinessObjects;
using TrainsInfo.Common.Interfaces;

namespace TrainsInfo.DataParser.JointAllVagon
{
    public class JointAllVagonDataParser : IDataParser
    {
        private readonly string rowPattern = @"\s*'([0-9]+)'\s*;\s*'([0-9]+)'\s*;\s*'([0-9]+)'\s*$";
        private readonly string FormationVagon = "OB_VG";

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
                        var rowMatch = Regex.Match(record, rowPattern);
                        if (rowMatch.Success)
                        {
                            var station1 = int.Parse(rowMatch.Groups[1].Value);
                            var findInfrastructure = infrastructures.Where(x => x.Type == TypeInfrastructure.joint && x.Station == station1).FirstOrDefault();
                            if (findInfrastructure != null)
                            {
                                var commonVagon = int.Parse(rowMatch.Groups[2].Value) + int.Parse(rowMatch.Groups[3].Value);
                                result.Add(new RowValue(station1, FormationVagon, commonVagon.ToString()));
                            }
                        }
                    }
                }
            }
            //
            return result;
        }

    }
}

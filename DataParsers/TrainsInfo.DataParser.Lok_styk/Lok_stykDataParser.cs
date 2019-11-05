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

namespace TrainsInfo.DataParser.Lok_styk
{
    public class Lok_stykDataParser : IDataParser
    {
        private readonly string rowPattern1 = @"\s*'.*'\s*;\s*'.*'\s*;\s*'.*'\s*;\s*'.*'\s*;\s*'.*'\s*;\s*'.*'\s*;\s*'.*'\s*;\s*'.*'\s*;\s*'.*'\s*;\s*'.*'\s*;\s*'.*'\s*;\s*'.*'\s*$";
        private readonly string rowPattern2 = @"\s*'.*'\s*;\s*;\s*'.*'\s*;\s*;\s*'.*'\s*;\s*;\s*'.*'\s*;\s*;\s*'.*'\s*;\s*;\s*'.*'\s*;\s*$";

        public IList<RowValue> Parse(object data, IList<InfrastructureBase> infrastructures)
        {
            var table = (BaseValue)data;
            var result = new List<RowValue>();
            if (table != null)
            {
                var parserValue = new StringBuilder();
                foreach (var record in table.Value.Split(new string[] { Environment.NewLine }, StringSplitOptions.None))
                {
                    if (Regex.IsMatch(record, rowPattern1) || Regex.IsMatch(record, rowPattern2))
                        parserValue.AppendLine(record.Replace("'", ""));
                }
                //
                result.Add(new RowValue(Constants.KeyTableLok_styk, parserValue.ToString(), table.LastUpdate));
            }
            //
            return result;
        }
    }
}

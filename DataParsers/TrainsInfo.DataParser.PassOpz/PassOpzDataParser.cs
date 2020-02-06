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

namespace TrainsInfo.DataParser.PassOpz
{
    public class PassOpzDataParser : IDataParser
    {
        private readonly string rowPattern = @"\s*'.*'\s*;\s*'.*'\s*;\s*'.*'\s*;\s*'.*'\s*;\s*'.*'\s*;\s*'.*'\s*$";

        public IList<RowValue> Parse(object data, IList<InfrastructureBase> infrastructures)
        {
            var table = (BaseValue)data;
            var result = new List<RowValue>();
            if (table != null)
            {
                var parserValue = new StringBuilder();
                var index = 0;
                var trainsPassOpz = new List<ModelTrainPassOpz>();
                foreach (var record in table.Value.Split(new string[] { Environment.NewLine }, StringSplitOptions.None))
                {
                    var rowMatch = Regex.Match(record, rowPattern);
                    if (rowMatch.Success)
                    {
                        var formatRecord = record.Replace("'", "");
                        parserValue.AppendLine(formatRecord);
                        if (index != 0)
                        {
                            var cells = formatRecord.Split(new char[] { ';' });
                            trainsPassOpz.Add(new ModelTrainPassOpz(cells[0], cells[2], cells[4], cells[5], formatRecord));
                        }
                        //
                        index++;
                    }
                }
                //
                result.Add(new RowValue(Constants.KeyTablePassOpz, parserValue.ToString(), table.LastUpdate, trainsPassOpz));
            }
            //
            return result;
        }
    }
}

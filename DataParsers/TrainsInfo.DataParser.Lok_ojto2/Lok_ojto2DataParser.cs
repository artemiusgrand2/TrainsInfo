﻿using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using TrainsInfo.Common.Enums;
using TrainsInfo.Common.BusinessObjects;
using TrainsInfo.Common.Interfaces;
using TrainsInfo.Common.Infrastructures;

namespace TrainsInfo.DataParser.Lok_ojto2
{
    public class Lok_ojto2DataParser : IDataParser
    {
        private readonly string rowPattern = @"\s*'\w*'\s*;\s*'.*'\s*;\s*'.*'\s*$";

        public IList<RowValue> Parse(object data, IList<InfrastructureBase> infrastructures)
        {
            var table = (BaseValue)data;
            var result = new List<RowValue>();
            if (table != null)
            {
                var parserValue = new StringBuilder();
                foreach (var record in table.Value.Split(new string[] { Environment.NewLine }, StringSplitOptions.None))
                {
                    var rowMatch = Regex.Match(record, rowPattern);
                    if (rowMatch.Success)
                        parserValue.AppendLine(record.Replace("'", ""));
                }
                //
                result.Add(new RowValue(Constants.KeyTableLok_ojto2, parserValue.ToString(), table.LastUpdate));
            }
            //
            return result;
        }
    }
}
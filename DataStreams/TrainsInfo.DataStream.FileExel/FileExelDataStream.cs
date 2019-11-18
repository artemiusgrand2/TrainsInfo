using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using TrainsInfo.Common.Attributes;
using TrainsInfo.Configuration.Records;
using TrainsInfo.Common.BusinessObjects;
using TrainsInfo.Common.Interfaces;

using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace TrainsInfo.DataStream.FileExel
{
    public class FileExelDataStream : IDataStream
    {
        private readonly string filePath;

        public string Info { get; } = string.Empty;

        public bool IsOnceConnect { get; }

        public FileExelDataStream(DataStreamRecord record)
        {
            filePath = record.ConnectionString;
            RemoteConnection.Connect(record);
        }

        public bool Read(out object data)
        {
            data = null;
            try
            {
                if (System.IO.File.Exists(filePath))
                {
                    HSSFWorkbook hssfwb;
                    using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                    {
                        hssfwb = new HSSFWorkbook(file);
                    }
                    //
                    ISheet sheet = hssfwb.GetSheetAt(0);
                     var result = new StringBuilder();
                    for (int row = 0; row <= sheet.LastRowNum; row++)
                    {
                        if (sheet.GetRow(row) != null) 
                        {
                            sheet.GetRow(row).Cells.ForEach(x => 
                            {
                                result.Append(string.Format("{0}{1}", x.ToString(), (sheet.GetRow(row).Cells.IndexOf(x) == sheet.GetRow(row).Cells.Count - 1) ?Environment.NewLine:";"));
                            }
                            );
                        }
                    }
                    //
                    data = new BaseValue(result.ToString(), System.IO.File.GetLastWriteTime(filePath));
                    return true;
                }
                else
                {
                    if (Logger.Log != null)
                        Logger.Log.LogError("Не найден файл по адресу - {0}", filePath);
                }
            }
            catch (Exception error)
            {
                if (Logger.Log != null)
                    Logger.Log.LogError(error.Message, error);
            }
            //
            return false;
        }



        public int Write(object data)
        {
            return 0;
        }

        public void Dispose() { }


    }

}


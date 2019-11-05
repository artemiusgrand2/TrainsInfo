using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using TrainsInfo.Common.Attributes;
using TrainsInfo.Configuration.Records;
using TrainsInfo.Common.BusinessObjects;
using TrainsInfo.Common.Interfaces;

namespace TrainsInfo.DataStream.File
{
    public class FileDataStream : IDataStream
    {
        private readonly string filePath;

        public string Info { get; } = string.Empty;

        public bool IsOnceConnect { get; }

        public FileDataStream(DataStreamRecord record)
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
                    data = new BaseValue(System.IO.File.ReadAllText(filePath, Constants.TextEncoding), System.IO.File.GetLastWriteTime(filePath)) ;
                    return true;
                }
                else
                {
                    if(Logger.Log != null)
                        Logger.Log.LogError("Не найден файл по адресу - {0}", filePath);
                }
            }
            catch(Exception error)
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

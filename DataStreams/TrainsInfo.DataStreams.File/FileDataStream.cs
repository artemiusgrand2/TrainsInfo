using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using TrainsInfo.Common.Attributes;
using TrainsInfo.Configuration.Records;
using TrainsInfo.Common.BusinessObjects;
using TrainsInfo.Common.Interfaces;

namespace TrainsInfo.DataStreams.File
{
    public class FileDataStream : IDataStream
    {
        private readonly string filePath;
      

        public FileDataStream(DataStreamRecord record)
        {
            string[] parts = record.ConnectionString.Split(':');
            if (parts.Length == 1 || parts.Length == 3)
            {
                if (parts.Length == 3)
                {
                    using (var webClient = new WebClient())
                    {
                        webClient.Credentials = new NetworkCredential(parts[1], parts[2]);
                    }
                }
                //
                filePath = parts[0];
            }
            else
                throw new InvalidDataException(string.Format("Неверная формат строки подключения - {0}", record.ConnectionString));
        }

        public bool Read(out object data)
        {
            data = null;
            try
            {
                if (System.IO.File.Exists(filePath))
                {
                    data = System.IO.File.ReadAllLines(filePath, Constants.TextEncoding);
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

        public void Dispose() { }
    }

}

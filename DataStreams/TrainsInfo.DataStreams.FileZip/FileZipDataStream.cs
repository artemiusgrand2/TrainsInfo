using System;
using System.Text;
using System.IO;
using System.Linq;
using System.IO.Compression;
using System.Net;
using System.Collections.Generic;
using TrainsInfo.Common.Attributes;
using TrainsInfo.Common.BusinessObjects;
using TrainsInfo.Configuration.Records;
using TrainsInfo.Common.Interfaces;

namespace TrainsInfo.DataStream.FileZip
{
    public class FileZipDataStream : IDataStream
    {
        private readonly string filePath;

        public string Info { get; } = string.Empty;

        public bool IsOnceConnect { get; }

        public FileZipDataStream(DataStreamRecord record)
        {
            filePath = record.ConnectionString;
            if (!string.IsNullOrEmpty(record.Login) && !string.IsNullOrEmpty(record.Password))
            {
                using (var webClient = new WebClient())
                {
                    webClient.Credentials = new NetworkCredential(record.Login, record.Password);
                }
            }
        }

        public bool Read(out object data)
        {
            data = null;
            try
            {
                if (File.Exists(filePath))
                {
                    data = System.IO.File.ReadAllLines(filePath);
                    using (var archive = ZipFile.Open(filePath, ZipArchiveMode.Read, Encoding.UTF8))
                    {
                        var entry = archive.Entries.FirstOrDefault();
                        if(entry != null)
                        {
                            var reads = new List<string>();
                            using (var reader = new StreamReader(entry.Open(), Constants.TextEncoding))
                            {
                                string input;
                                using (reader)
                                {
                                    while ((input = reader.ReadLine()) != null)
                                    {
                                        reads.Add(input);
                                    }
                                }
                            }
                            //
                            data = reads.ToArray();
                            return true;
                        }
                        else
                        {
                            if (Logger.Log != null)
                                Logger.Log.LogError("Не найден архив по адресу - {0}", filePath);
                        }
                    }
                }
                else
                {
                    if (Logger.Log != null)
                        Logger.Log.LogError("В архив по адресу - {0} не найден файл - отчета", filePath);
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

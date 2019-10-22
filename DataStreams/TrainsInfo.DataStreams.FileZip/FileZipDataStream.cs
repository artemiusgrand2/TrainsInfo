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

namespace TrainsInfo.DataStreams.FileZip
{
    public class FileZipDataStream : IDataStream
    {
        private readonly string filePath;
        private readonly string fileIn = "Отчет 1.csv";

        public FileZipDataStream(DataStreamRecord record)
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
                //if (!System.IO.File.Exists(parts[0]))
                //    throw new FileNotFoundException(string.Format("Не найден архив по адресу - {0}", parts[0]));
                ////
                //
                ////
                //using (var archive = ZipFile.Open(filePath, ZipArchiveMode.Update, Encoding.UTF8))
                //{
                //    if(archive.Entries.Where(x => x.FullName == fileIn).FirstOrDefault() == null)
                //        throw new FileNotFoundException(string.Format("В архив по адресу - {0} не найден файл - {1}", parts[0], fileIn));
                //}
            }
            else
                throw new InvalidDataException(string.Format("Неверная формат строки подключения - {0}", record.ConnectionString));
        }

        public bool Read(out object data)
        {
            data = null;
            try
            {
                if (File.Exists(filePath))
                {
                    data = System.IO.File.ReadAllLines(filePath);
                    using (var archive = ZipFile.Open(filePath, ZipArchiveMode.Update, Encoding.UTF8))
                    {
                        var entry = archive.Entries.Where(x => x.FullName == fileIn).FirstOrDefault();
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
                        Logger.Log.LogError("В архив по адресу - {0} не найден файл - {1}", filePath, fileIn);
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

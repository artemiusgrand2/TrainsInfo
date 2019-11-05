using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Collections.Generic;
using TrainsInfo.Common.Interfaces;
using TrainsInfo.Common.BusinessObjects;

namespace TrainsInfo.Common.BusinessObjects
{
    public class DataStorage
    {
        private Queue<RowValue> pendingInfos;
        private readonly string connectionString;
        private Thread thread;
        private bool stopFlag;

        public  DataStorage(string connectionString)
        {
            this.connectionString = connectionString;
            pendingInfos = new Queue<RowValue>();
            stopFlag = true;
            thread = new Thread(work);
        }

        public void Start()
        {
            if (!stopFlag)
                return;
            stopFlag = false;
            thread.Start();
        }
        public void Stop()
        {
            stopFlag = true;
            thread.Join();
        }

        private void work()
        {
            try
            {
                while (!stopFlag)
                {
                    if (pendingInfos.Count == 0)
                    {
                        Thread.Sleep(1000);
                        continue;
                    }
                    //
                    while (pendingInfos.Count > 0)
                    {
                        lock (pendingInfos)
                        {
                            UpdateInfo(pendingInfos.Dequeue());
                        }
                    }
                }
            }
            catch (Exception error)
            {
                Logger.Log.LogError(error.Message, error);
            }
        }

        public void ProcessValueChanged(IList<RowValue> infos)
        {
            lock (pendingInfos)
            {
                foreach (var info in infos)
                    pendingInfos.Enqueue(info);
            }
        }

        private void UpdateInfo(RowValue info)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var tr = connection.BeginTransaction())
                {
                    using (var command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.Transaction = tr;
                        //Добавление параметров
                        var nameParam1 = new SqlParameter("@Station1", info.Station1);
                        command.Parameters.Add(nameParam1);
                        var nameParam2 = new SqlParameter("@Station2", info.Station2);
                        command.Parameters.Add(nameParam2);
                        var nameParam3 = new SqlParameter("@Name", info.Name);
                        command.Parameters.Add(nameParam3);
                        var nameParam4 = new SqlParameter("@Value", info.Value);
                        command.Parameters.Add(nameParam4);
                        var nameParam5 = new SqlParameter("@TimeUpdate", info.LastUpdate);
                        command.Parameters.Add(nameParam5);
                        //проверяем есть ли запись в базе данных
                        command.CommandText = "UPDATE TabloValue SET Value = @Value, TimeUpdate = @TimeUpdate  Where Station1 = @Station1 and Station2 = @Station2 and Name = @Name";
                        if (command.ExecuteNonQuery() == 0)
                        {
                            command.CommandText = "INSERT INTO TabloValue (Station1, Station2, Name, Value, TimeUpdate) VALUES (@Station1, @Station2, @Name, @Value, @TimeUpdate)";
                            command.ExecuteNonQuery();
                        }
                    }
                    //
                    tr.Commit();
                }
                connection.Close();
            }
        }
    }
}

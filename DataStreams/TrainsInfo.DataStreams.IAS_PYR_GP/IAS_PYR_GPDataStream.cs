using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;
using TrainsInfo.Common.Attributes;
using TrainsInfo.Configuration.Records;
using TrainsInfo.Common.BusinessObjects;
using TrainsInfo.Common.Interfaces;

namespace TrainsInfo.DataStream.IAS_PYR_GP
{
    public class IAS_PYR_GPDataStream : IDataStream
    {
        private readonly string connectionString;

        public string Info { get; } = string.Empty;

        public bool IsOnceConnect { get; }

        public IAS_PYR_GPDataStream(DataStreamRecord record)
        {
            connectionString = record.ConnectionString;
        }

        public bool Read(out object data)
        {
            data = null;
            // название процедуры
            string sqlExpression = "GetLastMessages5676FromTime";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand(sqlExpression, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    // параметры
                    var nameParam1 = new SqlParameter("@StartTime", DateTime.Now.AddDays(-1));
                    var nameParam2 = new SqlParameter("@EndTime", DateTime.Now);
                    // добавляем параметры
                    command.Parameters.Add(nameParam1);
                    command.Parameters.Add(nameParam2);
                    //
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            var common = new List<ModelBase>();
                            while (reader.Read())
                            {
                                try
                                {
                                    int trainNumber;
                                    if (int.TryParse(reader.IsDBNull(1) ? string.Empty : reader.GetString(1), out trainNumber) &&
                                        ((trainNumber >= 1001 && trainNumber <= 3500) || (trainNumber >= 9201 && trainNumber <= 9799) || (trainNumber >= 1 && trainNumber <= 999) || (trainNumber >= 6000 && trainNumber <= 7628) || trainNumber == 9999))
                                    {
                                        var newValue = new ModelDataIAS_PYR_GP()
                                        {
                                            TrainId = reader.IsDBNull(0) ? string.Empty : reader.GetString(0),
                                            TrainNumber = trainNumber,
                                            StationCode = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                                            OperationCode = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                                            OperationTime = reader.IsDBNull(4) ? DateTime.MinValue : reader.GetDateTime(4),
                                            Index1 = reader.IsDBNull(5) ? string.Empty : reader.GetString(5),
                                            Index2 = reader.IsDBNull(6) ? string.Empty : reader.GetString(6),
                                            Index3 = reader.IsDBNull(7) ? string.Empty : reader.GetString(7),
                                            DirectionFromStation = reader.IsDBNull(8) ? string.Empty : reader.GetString(8),
                                            DirectionToStation = reader.IsDBNull(9) ? string.Empty : reader.GetString(9),
                                        };
                                        int buffferIndex1, buffferIndex3;
                                        int.TryParse(newValue.Index1, out buffferIndex1);
                                        int.TryParse(newValue.Index3, out buffferIndex3);
                                        if (!(buffferIndex1 <= 9 || buffferIndex3 <= 9))
                                            common.Add(newValue);
                                    }
                                }
                                catch { }
                            }
                            data = common;
                        }
                    }
                }
            }
            catch (Exception error)
            {
                return false;
            }
            //
            return true;
        }

        public int Write(object data)
        {
            return 0;
        }

        public void Dispose() { }
    }

}

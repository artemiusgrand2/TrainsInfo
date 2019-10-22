using System;
using System.IO;
using System.Collections.Generic;
using System.Data.SqlClient;
using TrainsInfo.Common.Attributes;
using TrainsInfo.Configuration.Records;
using TrainsInfo.Common.BusinessObjects;
using TrainsInfo.Common.Interfaces;

namespace TrainsInfo.DataStreams.IAS_PYR_GP
{
    public class IAS_PYR_GPDataStream : IDataStream
    {
        private readonly string connectionString;

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
                            var common = new List<ModelDataIAS_PYR_GP>();
                            while (reader.Read())
                            {
                                try
                                {
                                    common.Add(new ModelDataIAS_PYR_GP()
                                    {
                                        TrainId = reader.IsDBNull(0) ? string.Empty : reader.GetString(0),
                                        TrainNumber = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                                        StationCode = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                                        OperationCode = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                                        OperationTime = reader.IsDBNull(4) ? DateTime.MinValue : reader.GetDateTime(4),
                                        Index1 = reader.IsDBNull(5) ? string.Empty : reader.GetString(5),
                                        Index2 = reader.IsDBNull(6) ? string.Empty : reader.GetString(6),
                                        Index3 = reader.IsDBNull(7) ? string.Empty : reader.GetString(7),
                                        DirectionFromStation = reader.IsDBNull(8) ? string.Empty : reader.GetString(8),
                                        DirectionToStation = reader.IsDBNull(9) ? string.Empty : reader.GetString(9),
                                    });
                                }
                                catch { }
                            }
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

        public void Dispose() { }
    }

}

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HealthCheck
{
    public class SqlServerHealthCheck : IHealthCheck
    {
        private string _connectionString;

        public SqlServerHealthCheck(string connectionString)
        {
            _connectionString = connectionString;
        }

        public HealthCheckResult Check()
        {
            var result = new HealthCheckResult();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "sp_executesql";
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@stmt", "SELECT 1 + 1"));
                        connection.Open();
                        result.Status = (int)command.ExecuteScalar() == 2 ? Status.Healthy : Status.Unhealthy;
                    }
                }
            }
            catch
            {
                result.Status = Status.Unhealthy;
            }
            return result;
        }
    }
}
using System;
using Microsoft.Extensions.Configuration;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using SBS.Infrastructure.Connection.Dapper;

namespace SBS.Infrastructure.Connection.Utility
{
    public class ConnectionManager : BaseDapper
    {
        public ConnectionManager(IConfiguration _configuration) : base(_configuration)
        {
            _dapperConfiguration = _configuration;
        }

        static internal IDbConnection GetConnection()
        {
            string connectionStringName = "ConnectionStrings:ConexionSvr_Default";

            var connectionString = _dapperConfiguration[connectionStringName];

            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException("El parámetro connectionString se encuentra nulo.");

            return new OracleConnection(connectionString);
        }
    }
}

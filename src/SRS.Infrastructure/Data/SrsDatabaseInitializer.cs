using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace SRS.Infrastructure.Data
{
    public class SrsDatabaseInitializer
    {
        public void Initialize(IConfiguration configuration)
        {
            string master = configuration.GetConnectionString("MasterConnection");
            string srs = configuration.GetConnectionString("SrsConnection");
            string databaseName = configuration.GetSection("DataStore")["DatabaseName"];

            string createDatabaseSql = 
            $@"IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = '{databaseName}')
            BEGIN
                CREATE DATABASE ""{databaseName}""
            END";

            using (var connection = new SqlConnection(master))
            {
                connection.Open();
                try
                {
                    //can be implemented with drop
                    connection.Execute(createDatabaseSql);
                }
                catch (Exception ex)
                {

                }
            }

            using (var connection = new SqlConnection(srs))
            {
                connection.Open();

            }

        }
    }
}

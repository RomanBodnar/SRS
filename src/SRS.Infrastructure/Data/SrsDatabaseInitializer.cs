using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Dapper;
using Microsoft.Extensions.Configuration;
using SRS.Infrastructure.Options;

namespace SRS.Infrastructure.Data
{
    public class SrsDatabaseInitializer
    {
        public void Initialize(DatastoreOptions options)
        {
            string databaseName = options.DatabaseName;

            string createDatabaseSql = 
            $@"IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = '{databaseName}')
            BEGIN
                CREATE DATABASE ""{databaseName}""
            END";

            using (var connection = new SqlConnection(options.MasterConnection))
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

            using (var connection = new SqlConnection(options.SrsConnection))
            {
                connection.Open();

            }

        }
    }
}

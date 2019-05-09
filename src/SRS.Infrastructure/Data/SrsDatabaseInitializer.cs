using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Dapper;
using SRS.Core.Entities;
using SRS.Infrastructure.Options;

namespace SRS.Infrastructure.Data
{
    public class SrsDatabaseInitializer
    {
        //private readonly string ifTableNotExists = 

        Func<string, string, string> createTableIfNotExists =
            (tableName, tableCreationSql) =>
            {
                return 
                $@"IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'{tableName}')
                BEGIN
                    {tableCreationSql}
                END"; ;
            };

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

        private string CreatePhraseTableSql()
        {
            var tableName = "Phrases";
            var createPhrasesSql = 
                $@"CREATE TABLE {tableName}(
                    {nameof(Phrase.Id)} INT NOT NULL,
                    {nameof(Phrase.Term)} NVARCHAR NOT NULL,
                    {nameof(Phrase.Synonym)} NVARCHAR NULL,
                    {nameof(Phrase.Translation)} NVARCHAR NULL,
                    {nameof(Phrase.Meaning)} NVARCHAR NULL,
                    )";
            return
                $@"IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'{tableName}')
                BEGIN

                END";
        }

    }
}

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
        private readonly Func<string, string, string> createTableIfNotExists =
            (tableName, tableCreationSql) =>
            {
                return 
                $@"IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'{tableName}')
                BEGIN
                    {tableCreationSql}
                END"; ;
            };

        public void Initialize(DatastoreOptions options)
        {
            string databaseName = options.DatabaseName;

            string dropDatabaseSql = 
            $@"IF EXISTS(SELECT * FROM sys.databases WHERE name = '{databaseName}')
            BEGIN
                ALTER DATABASE [{databaseName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE
                DROP DATABASE [{databaseName}] 
            END";


            string createDatabaseSql = 
            $@"IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = '{databaseName}')
            BEGIN
                CREATE DATABASE [{databaseName}]
            END";



            using (var connection = new SqlConnection(options.Connection))
            {
                connection.Open();
                connection.Execute($"USE [{options.InitialCatalog}]");
                try
                {
                    connection.Execute(dropDatabaseSql);
                }
                catch (Exception ex)
                {
                    // todo: log error
                }
                finally
                {
                    // todo: move to try
                    connection.Execute(createDatabaseSql);
                    connection.Execute($"USE [{options.DatabaseName}]");
                    connection.Execute(this.CreatePhraseTableSql("Phrases"));
                }
            }
        }

        private string CreatePhraseTableSql(string tableName)
        {
            // todo: include schema
            var createPhrasesSql = 
                $@"CREATE TABLE {tableName}(
                    {nameof(Phrase.Id)} INT IDENTITY(1,1) NOT NULL,
                    {nameof(Phrase.Term)} NVARCHAR(200) NOT NULL,
                    {nameof(Phrase.Synonym)} NVARCHAR(200),
                    {nameof(Phrase.Translation)} NVARCHAR(200),
                    {nameof(Phrase.Meaning)} NVARCHAR(1000),
                   CONSTRAINT PK_{tableName} PRIMARY KEY ({nameof(Phrase.Id)})
                   )";

            return this.createTableIfNotExists(tableName, createPhrasesSql);
        }
    }
}

using ResSched.DataModel;
using ResSched.Interfaces;
using SQLite;
using System;
using System.Threading.Tasks;

namespace ResSched.Database
{
    public sealed class Database : IDatabase
    {
        public Database()
        {
        }

        public SQLiteAsyncConnection AsyncConnection { get; private set; }

        public SQLiteConnection Connection { get; private set; }

        public void CreateTables()
        {
            try
            {
                if (Connection != null)
                {
                    Connection.CreateTable<Resource>();
                    Connection.CreateTable<ResourceSchedule>();
                    Connection.CreateTable<User>();

                    Connection.CreateTable<Queue>();
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error creating sqlite database tables. {ex.Message}");
            }
        }

        public async Task DropTablesAsync()
        {
            try
            {
                if (AsyncConnection != null)
                {
                    await AsyncConnection.DropTableAsync<Resource>();
                    await AsyncConnection.DropTableAsync<ResourceSchedule>();
                    await AsyncConnection.DropTableAsync<User>();

                    await AsyncConnection.DropTableAsync<Queue>();
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error creating sqlite database tables. {ex.Message}");
            }
        }

        public SQLiteAsyncConnection GetAsyncConnection()
        {
            return AsyncConnection;
        }

        public SQLiteConnection GetConnection()
        {
            return Connection;
        }

        public async Task RestoreCurrentUserDatabaseAsync()
        {
            await DropTablesAsync();
            CreateTables();
        }

        public void SetConnection(SQLiteConnection conn, SQLiteAsyncConnection asyncConn)
        {
            Connection = conn;
            AsyncConnection = asyncConn;
        }
    }
}
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using TestingWorkshopAPI.DataAccess;

namespace TestingWorskshopTests.Setup
{
    public class SQLiteInMemoryContext
    {
        //public Create(string databaseName)
        //{
        //    string connectionString = $"DataSource={databaseName};Mode=Memory;Cache=Shared";

        //    var connection = new SqliteConnection(connectionString);
        //}

        public TestingWorkshopContext Create(string databaseName)
        {
            //var connection = new SqliteConnection("DataSource=:memory:");
            var connection = new SqliteConnection($"DataSource={databaseName};Mode=Memory;Cache=Shared");
            connection.Open();

            var option = new DbContextOptionsBuilder<TestingWorkshopContext>().UseSqlite(connection).Options;

            var context = new TestingWorkshopContext(option);

            if (context != null)
            {
                //context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }

            return context;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using TestingWorkshopAPI.DataAccess;

namespace TestingWorskshopTests.Setup
{
    public class InMemoryContext
    {
        public TestingWorkshopContext Create(string databaseName)
        {
            DbContextOptions<TestingWorkshopContext> options;
            var builder = new DbContextOptionsBuilder<TestingWorkshopContext>();
            builder.UseInMemoryDatabase(databaseName: databaseName);
            options = builder.Options;
            TestingWorkshopContext context = new TestingWorkshopContext(options);
            context.Database.EnsureCreated();

            return context;
        }
    }
}

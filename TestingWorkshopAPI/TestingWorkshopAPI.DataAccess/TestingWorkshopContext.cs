using Microsoft.EntityFrameworkCore;
using TestingWorkshop.Core.Entities;

namespace TestingWorkshopAPI.DataAccess
{
    public class TestingWorkshopContext : DbContext
    {
        public TestingWorkshopContext(DbContextOptions<TestingWorkshopContext> options) : base(options)
        {

        }

        public DbSet<Camera> Camera { get; set; }
        public DbSet<Lenses> Lenses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Camera
            //modelBuilder.Entity<Camera>()
            //    .HasKey(x => x.Id);

            //modelBuilder.Entity<Camera>()
            //    .Property(x => x.Manufacturer)
            //    .IsRequired();

            //modelBuilder.Entity<Camera>()
            //    .Property(x => x.Model)
            //    .IsRequired();
            #endregion

            #region Lenses
            modelBuilder.Entity<Lenses>()
                .Property(x => x.CompatibleCameraName)
                .IsRequired();

            modelBuilder.Entity<Lenses>()
                .Property(x => x.Manufacturer)
                .IsRequired();

            modelBuilder.Entity<Lenses>()
                .Property(x => x.Model)
                .IsRequired();
            #endregion
        }
    }
}

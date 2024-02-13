using MyProject.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace MyProject.DataAccess.Data
{
    public class ApplicationDbContext : DbContext 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Category> categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(

                new Category { Id=1, Name="Action",DisplayOrder=1},
                new Category { Id = 2, Name = "Sci/Fi", DisplayOrder = 2 },
                new Category { Id = 3, Name = "Romance", DisplayOrder = 3 },
                 new Category { Id = 4, Name = "History", DisplayOrder = 4}

                );
        }
    }
}

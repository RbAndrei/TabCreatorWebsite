using Microsoft.EntityFrameworkCore;

namespace TabCreator.Models
{
    public class TabCreatorContext(DbContextOptions<TabCreatorContext> options) : DbContext(options)
    {
        public DbSet<User>? Users { get; set; }
        public DbSet<Tablature>? Tablatures { get; set; }
        public DbSet<Sheet>? Sheets { get; set; }
        public DbSet<Chords>? Chords { get; set; }

/*        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // seeding of data
            modelBuilder.Entity<Location>().HasData(
                new Location { LocationId = 1, IsNumber = true, LocationName = "Location 1" },
                new Location { LocationId = 2, IsNumber = true, LocationName = "Location 2" },
                new Location { LocationId = 3, IsNumber = false, LocationName = "Location A" },
                new Location { LocationId = 4, IsNumber = false, LocationName = "Location B" }
            );
        }*/
    }
}

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TabCreator.Models
{
    public class TabCreatorContext(DbContextOptions<TabCreatorContext> options) : IdentityDbContext(options)
    {
        public DbSet<Tablature>? Tablatures { get; set; }
        public DbSet<Sheet>? Sheets { get; set; }
        public DbSet<Chords>? Chords { get; set; }
    }
}

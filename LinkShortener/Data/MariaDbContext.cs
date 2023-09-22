using LinkShortener.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace LinkShortener.Data
{
    public class MariaDbContext : DbContext
    {
        public DbSet<LinkModel> Links { get; set; }
        public MariaDbContext(DbContextOptions options) : base(options) 
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LinkModel>().HasData(
                    new LinkModel { Id = 1, RawUrl = "InitialURL", ShortUrl = "ShortUrl", Viewed = 0 }
            );
        }
    }
}

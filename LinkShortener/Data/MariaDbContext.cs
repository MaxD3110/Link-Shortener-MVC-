using LinkShortener.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace LinkShortener.Data
{
    public class MariaDbContext : DbContext
    {
        public DbSet<LinkModel> Links { get; set; }
        public MariaDbContext(DbContextOptions options) : base(options) 
        {
            try { Database.Migrate(); }
            catch (Exception ex) 
            {
                throw new Exception("Attention!!! Check Database connection string (appsettings.json) for UserID/Password/Host validity! \n" +
                    "This program requires MariaDB v10.3.39 to be installed on the machine to make a connection! \n" +
                    "Original exception message: " + ex.Message); 
            }
             
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LinkModel>().HasData(new LinkModel[]
            {
                new LinkModel { Id = 1, RawUrl = "https://google.com", ShortUrl = "38fh43", Viewed = 0 },
                new LinkModel { Id = 2, RawUrl = "https://youtube.com", ShortUrl = "fG47Kd", Viewed = 420 },
                new LinkModel { Id = 3, RawUrl = "https://avtobus1.ru/avtopark/avtobus/45-mest", ShortUrl = "98CsfD", Viewed = 1337 }
            });
        }
    }
}

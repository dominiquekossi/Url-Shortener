using Microsoft.EntityFrameworkCore;
using Url_Shortener.Entites;
using Url_Shortener.Services;

namespace Url_Shortener.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ShortenerUrl> shortenerUrls { get; set; }
       protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShortenerUrl>(builder =>
            {
                builder.Property(s => s.code).HasMaxLength(UrlShorteningService.NumberOfCharacter);
                builder.HasIndex(s => s.code).IsUnique();
            });
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            SQLitePCL.Batteries_V2.Init();
        }

    }
}

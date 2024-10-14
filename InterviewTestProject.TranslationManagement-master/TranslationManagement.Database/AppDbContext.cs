using Microsoft.EntityFrameworkCore;
using TranslationManagement.Database.Entities;

namespace TranslationManagement.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<TranslationJobEntity> TranslationJobs { get; set; }
        public DbSet<TranslatorEntity> Translators { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TranslationJobEntity>();
            modelBuilder.Entity<TranslatorEntity>();
        }
    }
}
using Microsoft.EntityFrameworkCore;
using KodeKloudApp.Models;

namespace KodeKloudApp.Data
{
    public class XnetDbContext : DbContext
    {
        public XnetDbContext(DbContextOptions<XnetDbContext> options)
            : base(options)
        {
        }

        public DbSet<AspNetUser> AspNetUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure AspNetUser entity
            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasMaxLength(255);
                entity.Property(e => e.FirstName).IsRequired();
                entity.Property(e => e.LastName).IsRequired();
                entity.Property(e => e.CountryCode).IsRequired();
                entity.Property(e => e.NationalNumber).IsRequired();
                entity.Property(e => e.CreatedAt).HasColumnType("datetime(6)");
                entity.Property(e => e.LastLoginAt).HasColumnType("datetime(6)");
                entity.Property(e => e.UserName).HasMaxLength(256);
                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
                entity.Property(e => e.Email).HasMaxLength(256);
                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
                entity.Property(e => e.LockoutEnd).HasColumnType("datetime(6)");
                
                // Indexes
                entity.HasIndex(e => e.NormalizedUserName).IsUnique().HasDatabaseName("UserNameIndex");
                entity.HasIndex(e => e.NormalizedEmail).HasDatabaseName("EmailIndex");
            });
        }
    }
}

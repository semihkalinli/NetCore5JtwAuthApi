using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Autoking.Task2.Core.Models
{
    public partial class JwtBasicTwoContext : DbContext
    {
        public JwtBasicTwoContext()
        {
        }

        public JwtBasicTwoContext(DbContextOptions<JwtBasicTwoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost;Database=JwtBasicTwo;Trusted_Connection=True; User ID=sa;Password=asdqwe123!");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Turkish_CI_AS");

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Expiration)
                    .HasColumnType("datetime")
                    .HasColumnName("expiration");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(10)
                    .HasColumnName("firstname")
                    .IsFixedLength(true);

                entity.Property(e => e.Lastname)
                    .HasMaxLength(10)
                    .HasColumnName("lastname")
                    .IsFixedLength(true);

                entity.Property(e => e.Password)
                    .HasMaxLength(10)
                    .HasColumnName("password")
                    .IsFixedLength(true);

                entity.Property(e => e.Refleshtoken)
                    .HasMaxLength(10)
                    .HasColumnName("refleshtoken")
                    .IsFixedLength(true);

                entity.Property(e => e.Role)
                    .HasMaxLength(10)
                    .HasColumnName("role")
                    .IsFixedLength(true);

                entity.Property(e => e.Token)
                    .HasMaxLength(10)
                    .HasColumnName("token")
                    .IsFixedLength(true);

                entity.Property(e => e.Username)
                    .HasMaxLength(10)
                    .HasColumnName("username")
                    .IsFixedLength(true);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

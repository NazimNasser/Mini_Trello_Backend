using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using TodoApi.Models;

namespace TodoApi.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Card> Cards { get; set; } = null!;
        public DbSet<Status> Statuses { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Card>(entity =>{
                entity.ToTable("Cards");

                entity.HasOne(d=>d.User)
                .WithMany(d=>d.Cards)
                .HasForeignKey(d=>d.UserId)
                .HasConstraintName("FK_Card_User")
                .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<User>(entity =>{
                entity.ToTable("Users");

                entity.HasMany(d=>d.Cards)
                .WithOne(d=>d.User)
                .HasConstraintName("FK_User_Card")
                .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Status>(entity =>{
                entity.ToTable("Statuses");

                entity.HasMany(d=>d.Cards)
                .WithOne(d=>d.Status)
                .HasConstraintName("FK_Status_Card")
                .OnDelete(DeleteBehavior.Cascade);
            });

            base.OnModelCreating(builder);
        }
    }
}
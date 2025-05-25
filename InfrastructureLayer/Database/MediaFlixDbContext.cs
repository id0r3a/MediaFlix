using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureLayer.Database
{
    public class MediaFlixDbContext : DbContext
    {
        public MediaFlixDbContext(DbContextOptions<MediaFlixDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Media> Media { get; set; }
        public DbSet<Review> Reviews { get; set; }

        //Konfigurerar relationer mellan entiteter.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User -> Media (en användare kan ha många media)
            modelBuilder.Entity<Media>()
                .HasOne<User>(media => media.User)
                .WithMany(user => user.Media)
                .HasForeignKey(media => media.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // User -> Review (en användare kan ha många recensioner)
            modelBuilder.Entity<Review>()
                .HasOne(review => review.User)
                .WithMany(user => user.Reviews)
                .HasForeignKey(review => review.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Media -> Review (ett mediaobjekt kan ha många recensioner)
            modelBuilder.Entity<Review>()
                .HasOne(review => review.Media)
                .WithMany(media => media.Reviews)
                .HasForeignKey(review => review.MediaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

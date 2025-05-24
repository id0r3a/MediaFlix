using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureLayer.Database
{
    public class MediaFlixDbContext : DbContext
    {
        public MediaFlixDbContext(DbContextOptions<MediaFlixDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}

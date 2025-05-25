using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureLayer.Database
{
    //Seedar testdata till databasen om den är tom.
    public static class SeedData
    {
        public static async Task InitializeAsync(MediaFlixDbContext context)
        {
            //Skapa databasen om den inte finns
            await context.Database.MigrateAsync();

            if (context.Users.Any()) return; // Redan seedad

            // ======= Skapa Användare =======
            var users = new List<User>
            {
                new User { Username = "alice", Email = "alice@example.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword("password1") },
                new User { Username = "bob", Email = "bob@example.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword("password2") },
                new User { Username = "carla", Email = "carla@example.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword("password3") },
                new User { Username = "david", Email = "david@example.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword("password4") },
                new User { Username = "emma", Email = "emma@example.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword("password5") }
            };

            context.Users.AddRange(users);
            await context.SaveChangesAsync();

            // ======= Skapa Media =======
            var mediaItems = new List<Media>
            {
                new Media { Title = "The Hobbit", Genre = "Fantasy", Creator = "Tolkien", Type = MediaType.Book, Status = MediaStatus.Read, UserId = users[0].Id },
                new Media { Title = "Inception", Genre = "Sci-Fi", Creator = "Nolan", Type = MediaType.Movie, Status = MediaStatus.Watching, UserId = users[1].Id },
                new Media { Title = "Pride and Prejudice", Genre = "Classic", Creator = "Austen", Type = MediaType.Book, Status = MediaStatus.WantToRead, UserId = users[2].Id },
                new Media { Title = "Interstellar", Genre = "Sci-Fi", Creator = "Nolan", Type = MediaType.Movie, Status = MediaStatus.WantToWatch, UserId = users[3].Id },
                new Media { Title = "The Matrix", Genre = "Action", Creator = "Wachowskis", Type = MediaType.Movie, Status = MediaStatus.Read, UserId = users[4].Id }
            };


            context.Media.AddRange(mediaItems);
            await context.SaveChangesAsync();

            // ======= Skapa Recensioner =======
            var reviews = new List<Review>
            {
                new Review { Rating = 5, Comment = "En av mina favoriter!", MediaId = mediaItems[0].Id, UserId = users[0].Id },
                new Review { Rating = 4, Comment = "Mind-blowing koncept!", MediaId = mediaItems[1].Id, UserId = users[1].Id },
                new Review { Rating = 3, Comment = "Lite seg men vacker.", MediaId = mediaItems[2].Id, UserId = users[2].Id },
                new Review { Rating = 5, Comment = "Otroligt gripande film.", MediaId = mediaItems[3].Id, UserId = users[3].Id },
                new Review { Rating = 4, Comment = "Ett måste för sci-fi-fans.", MediaId = mediaItems[4].Id, UserId = users[4].Id }
            };

            context.Reviews.AddRange(reviews);
            await context.SaveChangesAsync();
        }
    }
}

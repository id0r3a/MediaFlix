using Bogus;
using DomainLayer.Models;
using InfrastructureLayer.Database;

public static class BogusSeeder
{
    public static void Seed(MediaFlixDbContext context)
    {
        // 1️⃣ Kolla om det finns användare, annars skapa en
        if (!context.Users.Any())
        {
            var testUser = new User
            {
                Username = "testuser",
                Email = "testuser@example.com",
                PasswordHash = "placeholder"
            };
            context.Users.Add(testUser);
            context.SaveChanges();
        }

        //  Hämta alla existerande UserId:n
        var userIds = context.Users.Select(u => u.Id).ToList();

        //  Kolla om Media redan finns, annars skapa
        if (context.Media.Any())
            return; // undvik dubbel-seedning

        //  Bygg faker för Media
        var mediaFaker = new Faker<Media>()
            .RuleFor(m => m.Title, f => f.Lorem.Sentence(3))
            .RuleFor(m => m.Genre, f => f.PickRandom("Fantasy", "Sci-Fi", "Drama", "Action", "Comedy"))
            .RuleFor(m => m.Description, f => f.Lorem.Paragraph())
            .RuleFor(m => m.Type, f => f.PickRandom("Book", "Movie"))
            .RuleFor(m => m.Creator, (f, m) => m.Type == "Book" ? f.Person.FullName : f.Name.FullName())
            .RuleFor(m => m.Status, f => f.PickRandom("Watching", "Read", "Planned"))
            .RuleFor(m => m.CreatedAt, f => f.Date.Past())
            .RuleFor(m => m.UserId, f => f.PickRandom(userIds)); // här använder vi existerande Id:n

        //  Generera och spara
        var mediaItems = mediaFaker.Generate(200); // 100 books + 100 movies (slumpas)
        context.Media.AddRange(mediaItems);
        context.SaveChanges();
    }
}

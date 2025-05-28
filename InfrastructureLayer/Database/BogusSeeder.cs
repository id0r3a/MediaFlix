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
            .RuleFor(m => m.Type, f => f.PickRandom(MediaType.All))
            .RuleFor(m => m.Creator, (f, m) => m.Type == MediaType.Book ? f.Name.FullName() : f.Company.CompanyName())
            .RuleFor(m => m.Status, f => f.PickRandom(MediaStatus.All))
            .RuleFor(m => m.CreatedAt, f => f.Date.Past())
            .RuleFor(m => m.UserId, f => f.PickRandom(userIds));

        var mediaList = mediaFaker.Generate(200);
        context.Media.AddRange(mediaList);
        context.SaveChanges();

        // Skapa recensioner för vissa media
        var reviewFaker = new Faker<Review>()
            .RuleFor(r => r.Rating, f => f.Random.Int(1, 5))
            .RuleFor(r => r.Comment, f => f.Lorem.Sentence(10));
            

        var reviewsToAdd = new List<Review>();

        foreach (var media in mediaList)
        {
            // 50 % chans att skapa en recension
            if (new Random().NextDouble() < 0.5)
            {
                var review = reviewFaker.Generate();
                review.MediaId = media.Id;
                review.UserId = media.UserId;
                reviewsToAdd.Add(review);
            }
        }

        context.Reviews.AddRange(reviewsToAdd);
        context.SaveChanges();
    }
}

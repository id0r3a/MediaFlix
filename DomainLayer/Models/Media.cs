namespace DomainLayer.Models
{
    public class Media
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Creator { get; set; }
        public string Type { get; set; } // "Book", "Movie"
        public string Status { get; set; } // "Watching", "Read", etc.
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Koppling till användaren (ägaren av posten)
        public int UserId { get; set; }
    }
}

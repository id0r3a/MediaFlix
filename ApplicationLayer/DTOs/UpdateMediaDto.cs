using System.ComponentModel.DataAnnotations;

namespace ApplicationLayer.DTOs
{
    public class UpdateMediaDto
    {
        public string? Title { get; set; }

        public string? Genre { get; set; }

        public string? Description { get; set; }

        public string? Creator { get; set; }

        public string? Type { get; set; }

        [RegularExpression("Watched|Read|WantToWatch|WantToRead", ErrorMessage = "Invalid status.")]
        public string? Status { get; set; }
    }

}

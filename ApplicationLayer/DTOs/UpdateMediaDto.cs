using System.ComponentModel.DataAnnotations;

namespace ApplicationLayer.DTOs
{
    public class UpdateMediaDto
    {
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Genre is required.")]
        public string Genre { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty ;

        public string Creator { get; set; } = string.Empty;

        [Required(ErrorMessage = "Type is required.")]
        [RegularExpression("Book|Movie", ErrorMessage = "Type must be 'Book' or 'Movie'.")]
        public string Type { get; set; } = string.Empty;

        [Required(ErrorMessage = "Status is required.")]
        [RegularExpression("Watched|Read|WantToWatch|WantToRead", ErrorMessage = "Invalid status.")]
        public string Status { get; set; } = string.Empty;
    }
}

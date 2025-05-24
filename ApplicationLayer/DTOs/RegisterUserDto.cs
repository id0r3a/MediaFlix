using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.DTOs
{
    public class RegisterUserDto
    {
        [Required(ErrorMessage = "Användarnamn krävs.")]
        [MinLength(3, ErrorMessage = "Användarnamnet måste vara minst 3 tecken.")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "E-post krävs.")]
        [EmailAddress(ErrorMessage = "Ogiltig e-postadress.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Lösenord krävs.")]
        [MinLength(6, ErrorMessage = "Lösenordet måste vara minst 6 tecken.")]
        public string Password { get; set; } = string.Empty;
    }
}

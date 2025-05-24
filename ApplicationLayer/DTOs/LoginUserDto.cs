using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.DTOs
{
    public class LoginUserDto
    {
        [Required(ErrorMessage = "E-post krävs.")]
        [EmailAddress(ErrorMessage = "Ogiltig e-postadress.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Lösenord krävs.")]
        public string Password { get; set; } = string.Empty;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class User
    {
        public int Id { get; set; }  // Primärnyckel

        public string Username { get; set; } = string.Empty; // Unikt användarnamn

        public string Email { get; set; } = string.Empty;    // Unik e-postadress

        public string PasswordHash { get; set; } = string.Empty; // Hashat lösenord

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        //Navigering: En användare kan ha flera media
        public ICollection<Media> Media { get; set; } = new List<Media>();

        //Navigering: En användare kan ha flera recensioner
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}

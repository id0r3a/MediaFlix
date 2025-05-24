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

        public string Username { get; set; }  // Unikt användarnamn

        public string Email { get; set; }     // Unik e-postadress

        public string PasswordHash { get; set; }  // Hashat lösenord

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Skapad-tid
    }
}

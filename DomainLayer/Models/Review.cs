using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class Review
    {
        public int Id { get; set; }

        public int Rating { get; set; }  // Betyg 1–5

        public string Comment { get; set; } = string.Empty;

        public int MediaId { get; set; }
        public Media Media { get; set; } = null!;

        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.DTOs
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
        public int MediaId { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
    }
}

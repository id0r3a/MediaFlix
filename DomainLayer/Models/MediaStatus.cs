using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public static class MediaStatus
    {
        public const string Read = "Read";
        public const string Watching = "Watching";
        public const string WantToRead = "WantToRead";
        public const string WantToWatch = "WantToWatch";

        public static readonly string[] All = { Read, Watching, WantToRead, WantToWatch };
    }
}

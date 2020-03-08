using SteamingService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteamingService.Entities
{
    public class StreamViewer
    {
        public int StreamId { get; set; }
        public Stream Stream { get; set; }

        public int ViewerId { get; set; }
        public User Viewer { get; set; }
    }
}

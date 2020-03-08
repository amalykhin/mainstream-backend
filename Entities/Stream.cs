using SteamingService.Models;
using System.Collections.Generic;

namespace SteamingService.Entities
{
    public class Stream
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public int BroadcasterId { get; set; }
        public User Broadcaster { get; set; }

        public ICollection<StreamViewer> Viewers { get; set; }
    }
}

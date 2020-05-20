using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteamingService.Models
{
    public class StartStreamModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Broadcaster { get; set; }
        public string StreamUri { get; set; }
    }

    public class EndStreamModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}

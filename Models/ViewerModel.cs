using SteamingService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteamingService.Models
{
    public class ViewerModel
    {
        public Stream Stream { get; set; }
        public User Viewer { get; set; }

        public void Deconstruct(out Stream stream, out User viewer)
        {
            stream = Stream;
            viewer = Viewer;
        }
    }
}

using SteamingService.Entities;
using System;
using System.Collections.Generic;

namespace SteamingService.Models
{
    public class User
    {
        public enum UserState { Offline, Active, Watching, Streaming };

        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public UserState State { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] HashSalt { get; set; }
        public string StreamerKey { get; set; }

        public ICollection<StreamViewer> StreamsWatching { get; set; }

        public UserState ResolveState(UserState state)
        {
            State = (User.UserState)Math.Max((byte)state, (byte)State);
            
            return State;
        }
    }
}

using SteamingService.Entities;
using SteamingService.Models;
using System.Collections.Generic;

namespace SteamingService.Services
{
    public interface IStreamService
    {
        Stream AddViewer(Stream streamInfo, User viewer);
        void EndStream(Stream streamInfo);
        Stream StartStream(Stream stream, User broadcaster = null);
        IEnumerable<Stream> GetStreams();
        Stream RemoveViewer(Stream streamInfo, User viewer);
        Stream StartStream(Stream stream, string broadcasterName = null);
    }
}
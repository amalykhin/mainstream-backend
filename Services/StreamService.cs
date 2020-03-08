using Microsoft.EntityFrameworkCore;
using SteamingService.Entities;
using SteamingService.Helpers;
using SteamingService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteamingService.Services
{
    public class StreamService : IStreamService
    {
        private DataContext _context;

        public StreamService(DataContext context)
        {
            _context = context;
        }

        public Stream AddViewer(Stream streamInfo, User viewer)
        {
            var stream = _context.Streams
                .Find(streamInfo.Id)
                ?? throw new ArgumentException("Stream doesn't exist");

            var streamViewer = new StreamViewer
            {
                Stream = stream,
                Viewer = viewer
            };
            stream.Viewers.Add(streamViewer);
            viewer.State = User.UserState.Watching;

            _context.SaveChanges();

            return stream;
        }

        public Stream RemoveViewer(Stream streamInfo, User viewer)
        {
            var stream = _context.Streams
                .Find(streamInfo.Id)
                ?? throw new ArgumentException("Stream doesn't exist");

            var streamViewer = new StreamViewer
            {
                Stream = stream,
                Viewer = viewer
            };
            stream.Viewers.Remove(streamViewer);
            viewer.State = viewer.StreamsWatching.Count > 0
                ? viewer.State
                : User.UserState.Active;

            _context.SaveChanges();

            return stream;
        }

        public void EndStream(Stream streamInfo)
        {
            var stream = _context.Streams.Find(streamInfo.Id)
                ?? throw new ArgumentException("Stream doesn't exist");
            _context.Streams.Remove(stream);
            stream.Broadcaster.State = User.UserState.Active;

            _context.SaveChanges();
        }

        public IEnumerable<Stream> GetStreams()
        {
            return _context.Streams
                .Include(s => s.Broadcaster);
        }

        public Stream StartStream(Stream stream, string broadcasterName = null)
        {
            var broadcaster = _context.Users
                .Where(u => u.Username == broadcasterName)
                .SingleOrDefault();

            return StartStream(stream, broadcaster);
        }

            public Stream StartStream(Stream stream, User broadcaster = null)
        {
            if (stream.Broadcaster == null && broadcaster == null)
            {
                throw new ArgumentException("A broadcaster must be set for a stream.");
            }

            stream.Broadcaster = broadcaster ?? stream.Broadcaster;
            stream.Broadcaster.State = User.UserState.Streaming;

            _context.Streams.Add(stream);

            _context.SaveChanges();

            return stream;
        }
    }
}

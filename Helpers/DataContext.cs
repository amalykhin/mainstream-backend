using Microsoft.EntityFrameworkCore;
using SteamingService.Entities;
using SteamingService.Models;

namespace SteamingService.Helpers
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StreamViewer>()
                .HasKey(t => new { t.StreamId, t.ViewerId });

            modelBuilder.Entity<StreamViewer>()
                .HasOne(sv => sv.Stream)
                .WithMany(s => s.Viewers)
                .HasForeignKey(sv => sv.StreamId);

            modelBuilder.Entity<StreamViewer>()
                .HasOne(sv => sv.Viewer)
                .WithMany(v => v.StreamsWatching)
                .HasForeignKey(sv => sv.ViewerId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Stream> Streams { get; set; }
    }
}

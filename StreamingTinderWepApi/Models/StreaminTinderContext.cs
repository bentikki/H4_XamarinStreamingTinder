using Microsoft.EntityFrameworkCore;
using StreamingTinderClassLibrary.StreamingService.Models;
using StreamingTinderWepApi.Entities;

namespace StreamingTinderWepApi.Models
{
    public class StreaminTinderContext : DbContext
    {
        public StreaminTinderContext(DbContextOptions<StreaminTinderContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<StreamingPlatformEntity> StreamingPlatforms { get; set; }
        public DbSet<RoomEntity> Rooms { get; set; }
    }
}

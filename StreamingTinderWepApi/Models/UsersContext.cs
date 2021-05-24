using Microsoft.EntityFrameworkCore;
using StreamingTinderWepApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StreamingTinderWepApi.Models
{
    public class StreaminTinderContext : DbContext
    {
        public StreaminTinderContext(DbContextOptions<StreaminTinderContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}

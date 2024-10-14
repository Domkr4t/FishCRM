using FIshCRM.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace FishCRM.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public DbSet<FishBase> FishBases { get; set; }
        public DbSet<Fish> Fishs { get; set; }


    }
}


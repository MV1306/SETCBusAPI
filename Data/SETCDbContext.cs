using Microsoft.EntityFrameworkCore;
using SETCBusAPI.Models;
using System.Net.Sockets;

namespace SETCBusAPI.Data
{
    public class SETCDbContext : DbContext
    {
        public SETCDbContext(DbContextOptions<SETCDbContext> options) : base(options) 
        {
            
        }

        public DbSet<BusRoute> BusRoutes { get; set; }
        public DbSet<BusRouteService> BusRouteServices { get; set; }

    }
}

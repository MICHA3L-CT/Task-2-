using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CityPointWeb.Models;

namespace CityPointWeb.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<CityPointWeb.Models.Booking> Booking { get; set; } = default!;
        public DbSet<CityPointWeb.Models.BookingStatusHistory> BookingStatusHistory { get; set; } = default!;
        public DbSet<CityPointWeb.Models.Room> Room { get; set; } = default!;
    }
}

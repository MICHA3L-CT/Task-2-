using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CityPoint.Models;

namespace CityPoint.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<CityPoint.Models.Booking> Booking { get; set; } = default!;
        public DbSet<CityPoint.Models.BookingStatusHistory> BookingStatusHistory { get; set; } = default!;
        public DbSet<CityPoint.Models.Room> Room { get; set; } = default!;
    }
}

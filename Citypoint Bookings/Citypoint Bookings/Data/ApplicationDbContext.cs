using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Citypoint_Bookings.Models;

namespace Citypoint_Bookings.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Citypoint_Bookings.Models.Booking> Booking { get; set; } = default!;
        public DbSet<Citypoint_Bookings.Models.BookingStatusHistory> BookingStatusHistory { get; set; } = default!;
        public DbSet<Citypoint_Bookings.Models.Room> Room { get; set; } = default!;
    }
}

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Task2.Models;

namespace CityPoint.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Task2.Models.Booking> Booking { get; set; } = default!;
        public DbSet<Task2.Models.BookingStatusHistory> BookingStatusHistory { get; set; } = default!;
        public DbSet<Task2.Models.Room> Room { get; set; } = default!;
        public DbSet<Task2.Models.Staff> Staff { get; set; } = default!;
    }
}

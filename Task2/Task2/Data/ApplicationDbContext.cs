using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Task2.Models;

namespace Task2.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Task2.Models.AvailabilityCalendar> AvailabilityCalendar { get; set; } = default!;
        public DbSet<Task2.Models.AvailabiltySlots> AvailabiltySlots { get; set; } = default!;
        public DbSet<Task2.Models.Booking> Booking { get; set; } = default!;
        public DbSet<Task2.Models.BookingStatusHistory> BookingStatusHistory { get; set; } = default!;
        public DbSet<Task2.Models.FAQ> FAQ { get; set; } = default!;
        public DbSet<Task2.Models.Room> Room { get; set; } = default!;
        public DbSet<Task2.Models.Staff> Staff { get; set; } = default!;
    }
}

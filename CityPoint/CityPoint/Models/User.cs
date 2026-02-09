using Microsoft.AspNetCore.Identity;

namespace CityPoint.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public ICollection<Booking>? Bookings { get; set; }

    }
}

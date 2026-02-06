using Citypoint_Bookings.Models;

namespace Citypoint_Bookings.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
        public string GuestName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string BookingStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public ICollection<BookingStatusHistory>? StatusHistory { get; set; }
    }
}

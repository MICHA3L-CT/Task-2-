namespace Task2.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public int RoomId { get; set; }
        public int StaffId { get; set; }    
        public string UserId { get; set; }
        public string GuestName { get; set; }
        public int NumberOfGuests { get; set; }
        public DateOnly BookingDate { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Room Room{ get; set; }
        public Staff Staff { get; set; }
        public ICollection<BookingStatusHistory> BookingStatusHistories { get; set; }



    }
}

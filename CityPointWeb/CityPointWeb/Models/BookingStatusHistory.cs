namespace CityPointWeb.Models
{
    public class BookingStatusHistory
    {
        public int BookingStatusHistoryId { get; set; }
        public int BookingId { get; set; }
        public string OldStatus { get; set; }
        public string NewStatus { get; set; }
        public DateTime ChangedAt { get; set; }
        public Booking? Booking { get; set; }

    }
}

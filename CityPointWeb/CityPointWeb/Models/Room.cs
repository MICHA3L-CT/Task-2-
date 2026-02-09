namespace CityPointWeb.Models
{
    public class Room
    {
        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public int Roomumber { get; set; }
        public int Capacity { get; set; }
        public string Description { get; set; }
        public decimal PricePerNight { get; set; }
        public decimal RoomSize { get; set; }
        public bool IsAvailable { get; set; }

        public ICollection<Booking>? Bookings { get; set; }

    }
}

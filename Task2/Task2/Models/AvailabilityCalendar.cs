namespace Task2.Models
{
    public class AvailabilityCalendar
    {
        public int AvailabilityCalendarId { get; set; }
        public int RoomId { get; set; }
        public DateOnly Date { get; set; }
        public Room Room { get; set; }
        public ICollection<AvailabiltySlots> AvailabiltySlots { get; set; }
    }
}

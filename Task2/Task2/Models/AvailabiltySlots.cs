namespace Task2.Models
{
    public class AvailabiltySlots
    {
        public int AvailabiltySlotsId { get; set; }
        public int AvailabilityCalendarId { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public bool IsAvailable { get; set; }
        public AvailabilityCalendar AvailabilityCalendar { get; set; }
    }
}

namespace Task2.Models
{
    public class Staff
    {
        public int StaffId { get; set; }
        public string userId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime HireDate { get; set; }

        public ICollection<Booking> Bookings { get; set; }
        public ICollection<FAQ> FAQs { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Task2.Models;

namespace Task2.Data
{
    public class SeedData
    {
        public static async Task SeedRoomsAsync(ApplicationDbContext context)
        {
            // Seed Rooms
            if (!await context.Room.AnyAsync())
            {
                var rooms = new List<Room>
                {
                    new Room
                    {
                        RoomName = "Deluxe Suite",
                        Roomumber = 101,
                        Capacity = 2,
                        Description = "A luxurious suite with ocean view.",
                        PricePerNight = 250,
                        RoomSize = 45.0m,
                        IsAvailable = true
                    },
                    new Room
                    {
                        RoomName = "Standard Room",
                        Roomumber = 102,
                        Capacity = 2,
                        Description = "A comfortable room with all basic amenities.",
                        PricePerNight = 100,
                        RoomSize = 25.0m,
                        IsAvailable = true
                    },
                    new Room
                    {
                        RoomName = "Deluxe Room",
                        Roomumber = 103,
                        Capacity = 2,
                        Description = "A spacious room with upgraded furnishings and city views.",
                        PricePerNight = 150,
                        RoomSize = 30.0m,
                        IsAvailable = false
                    },
                    new Room
                    {
                        RoomName = "Family Room",
                        Roomumber = 104,
                        Capacity = 4,
                        Description = "Ideal for families, featuring extra beds and a larger living area.",
                        PricePerNight = 180,
                        RoomSize = 40.0m,
                        IsAvailable = true
                    },

                    new Room
                    {
                        RoomName = "Executive Suite",
                        Roomumber = 201,
                        Capacity = 2,
                        Description = "A premium suite offering a separate seating area and luxury amenities.",
                        PricePerNight = 250,
                        RoomSize = 55.0m,
                        IsAvailable = true
                    },

                    new Room
                    {
                        RoomName = "Single Room",
                        Roomumber = 105,
                        Capacity = 1,
                        Description = "A compact room suitable for solo travellers.",
                        PricePerNight = 80,
                        RoomSize = 18.0m,
                        IsAvailable = false
                   },

                   new Room
                   {
                        RoomName = "Twin Room",
                        Roomumber = 106,
                        Capacity = 2,
                        Description = "A practical room with two single beds, ideal for colleagues or friends.",
                        PricePerNight = 120,
                        RoomSize = 28.0m,
                        IsAvailable = true
                   },

                   new Room
                   {
                        RoomName = "Presidential Suite",
                        Roomumber = 301,
                        Capacity = 4,
                        Description = "A luxury suite featuring multiple rooms, premium furnishings, and exclusive services.",
                        PricePerNight = 400,
                        RoomSize = 85.0m,
                        IsAvailable = true
                   }
                };
                await context.Room.AddRangeAsync(rooms);
                await context.SaveChangesAsync();
            }
           
            // Seed bookings
            public static async Task SeedBookingsAsync(ApplicationDbContext context)
            {
                if (!context.Booking.Any())
                {
                    context.Booking.AddRange(
                        new Models.Booking
                        {
                            RoomId = 1,
                            StaffId = 1,
                            UserId = "guest001",
                            GuestName = "Alice Smith",
                            NumberOfGuests = 2,
                            BookingDate = DateTime.Now.AddDays(-10),
                            CheckInDate = DateTime.Now.AddDays(5),
                            CheckOutDate = DateTime.Now.AddDays(10),
                            CreatedAt = DateTime.Now.AddDays(-10),
                            UpdatedAt = DateTime.Now.AddDays(-10)
                        },
                        new Models.Booking
                        {
                            RoomId = 2,
                            StaffId = 2,
                            UserId = "guest002",
                            GuestName = "Bob Johnson",
                            NumberOfGuests = 1,
                            BookingDate = DateTime.Now.AddDays(-8),
                            CheckInDate = DateTime.Now.AddDays(3),
                            CheckOutDate = DateTime.Now.AddDays(6),
                            CreatedAt = DateTime.Now.AddDays(-8),
                            UpdatedAt = DateTime.Now.AddDays(-8)
                        }
                    );
                    await context.SaveChangesAsync();
                }
        }
          
}

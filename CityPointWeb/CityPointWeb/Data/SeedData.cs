using CityPointWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CityPointWeb.Data
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
        }

        //Seed Bookings
        public static async Task SeedBookingsAsync(IServiceProvider serviceProvider, UserManager<IdentityUser> userManager)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            if (!context.Booking.Any())
            {
                var user1 = await userManager.FindByEmailAsync("user1@example.com");
                var user2 = await userManager.FindByEmailAsync("user2@example.com");


                if (user1 != null && user2 != null)
                {
                    var Bookings = new List<Booking>
                {
                        new Booking
                        {
                            RoomId = 1,
                            GuestName = "John Doe",
                            Email = "guest1@example.com",
                            PhoneNumber = "123-456-7890",
                            CheckInDate = DateTime.Now.AddDays(10),
                            CheckOutDate = DateTime.Now.AddDays(15),
                            TotalPrice = 1250,
                            BookingStatus = "Confirmed",
                            UserID = user1.Id
                        },
                        new Booking
                        {
                            RoomId = 2,
                            GuestName = "Jane Smith",
                            Email = "guest2@example.com",
                            PhoneNumber = "987-654-3210",
                            CheckInDate = DateTime.Now.AddDays(20),
                            CheckOutDate = DateTime.Now.AddDays(25),
                            TotalPrice = 500,
                            BookingStatus = "Pending",
                            UserID = user2.Id

                        }
                    };
                    await context.Booking.AddRangeAsync(Bookings);
                    await context.SaveChangesAsync();

                }


            }
        }
    }
}

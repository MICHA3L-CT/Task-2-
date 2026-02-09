using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CityPointWeb.Data;
using CityPointWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace CityPointWeb.Controllers
{
    [Authorize] // Require authentication for all actions
    public class BookingsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public BookingsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Bookings
        public async Task<IActionResult> Index()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isAdmin = User.IsInRole("Admin");

            IQueryable<Booking> bookingsQuery = _context.Booking.Include(b => b.Room);

            // If not admin, filter to show only current user's bookings
            if (!isAdmin)
            {
                bookingsQuery = bookingsQuery.Where(b => b.UserID == currentUserId);
            }

            return View(await bookingsQuery.ToListAsync());
        }

        // GET: Bookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking
                .Include(b => b.Room)
                .FirstOrDefaultAsync(m => m.BookingId == id);

            if (booking == null)
            {
                return NotFound();
            }

            // Check if user is authorized to view this booking
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isAdmin = User.IsInRole("Admin");

            if (!isAdmin && booking.UserID != currentUserId)
            {
                return Forbid(); // Return 403 Forbidden
            }

            return View(booking);
        }

        // GET: Bookings/Create
        public IActionResult Create()
        {
            // Only show available rooms
            ViewData["RoomId"] = new SelectList(_context.Set<Room>().Where(r => r.IsAvailable), "RoomId", "RoomName");
            return View();
        }

        // POST: Bookings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookingId,RoomId,GuestName,Email,PhoneNumber,CheckInDate,CheckOutDate,TotalPrice,BookingStatus")] Booking booking)
        {
            // Automatically set the UserID to the current logged-in user
            booking.UserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            booking.CreatedAt = DateTime.Now;
            booking.UpdatedAt = DateTime.Now;

            // Remove UserID from ModelState validation since we're setting it manually
            ModelState.Remove("UserID");

            if (ModelState.IsValid)
            {
                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["RoomId"] = new SelectList(_context.Set<Room>().Where(r => r.IsAvailable), "RoomId", "RoomName", booking.RoomId);
            return View(booking);
        }

        // GET: Bookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            // Check if user is authorized to edit this booking
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isAdmin = User.IsInRole("Admin");

            if (!isAdmin && booking.UserID != currentUserId)
            {
                return Forbid();
            }

            ViewData["RoomId"] = new SelectList(_context.Set<Room>(), "RoomId", "RoomName", booking.RoomId);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookingId,RoomId,GuestName,Email,PhoneNumber,CheckInDate,CheckOutDate,TotalPrice,BookingStatus")] Booking booking)
        {
            if (id != booking.BookingId)
            {
                return NotFound();
            }

            // Get the original booking to preserve UserID and check authorization
            var originalBooking = await _context.Booking.AsNoTracking().FirstOrDefaultAsync(b => b.BookingId == id);
            if (originalBooking == null)
            {
                return NotFound();
            }

            // Check if user is authorized to edit this booking
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isAdmin = User.IsInRole("Admin");

            if (!isAdmin && originalBooking.UserID != currentUserId)
            {
                return Forbid();
            }

            // Preserve the original UserID
            booking.UserID = originalBooking.UserID;
            booking.CreatedAt = originalBooking.CreatedAt;
            booking.UpdatedAt = DateTime.Now;

            // Remove UserID from ModelState validation
            ModelState.Remove("UserID");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(booking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.BookingId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["RoomId"] = new SelectList(_context.Set<Room>(), "RoomId", "RoomName", booking.RoomId);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking
                .Include(b => b.Room)
                .FirstOrDefaultAsync(m => m.BookingId == id);

            if (booking == null)
            {
                return NotFound();
            }

            // Check if user is authorized to delete this booking
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isAdmin = User.IsInRole("Admin");

            if (!isAdmin && booking.UserID != currentUserId)
            {
                return Forbid();
            }

            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var booking = await _context.Booking.FindAsync(id);

            if (booking != null)
            {
                // Check if user is authorized to delete this booking
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var isAdmin = User.IsInRole("Admin");

                if (!isAdmin && booking.UserID != currentUserId)
                {
                    return Forbid();
                }

                _context.Booking.Remove(booking);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool BookingExists(int id)
        {
            return _context.Booking.Any(e => e.BookingId == id);
        }
    }
}
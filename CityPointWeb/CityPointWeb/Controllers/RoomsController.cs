using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CityPointWeb.Data;
using CityPointWeb.Models;

namespace CityPointWeb.Controllers
{
    public class RoomsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RoomsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Rooms
        public async Task<IActionResult> Index(string searchString, int? minCapacity, int? maxCapacity,
            decimal? minPrice, decimal? maxPrice, bool? isAvailable, string sortOrder)
        {
            // Store current filter values in ViewData to maintain them in the view
            ViewData["CurrentFilter"] = searchString;
            ViewData["MinCapacity"] = minCapacity;
            ViewData["MaxCapacity"] = maxCapacity;
            ViewData["MinPrice"] = minPrice;
            ViewData["MaxPrice"] = maxPrice;
            ViewData["IsAvailable"] = isAvailable;
            ViewData["CurrentSort"] = sortOrder;

            // Set up sorting parameters for toggle functionality
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["PriceSortParm"] = sortOrder == "price" ? "price_desc" : "price";
            ViewData["CapacitySortParm"] = sortOrder == "capacity" ? "capacity_desc" : "capacity";
            ViewData["RoomNumberSortParm"] = sortOrder == "roomnumber" ? "roomnumber_desc" : "roomnumber";

            // Start with all rooms
            var rooms = from r in _context.Room
                        select r;

            // Apply search filter - searches in room name and description
            if (!String.IsNullOrEmpty(searchString))
            {
                rooms = rooms.Where(r => r.RoomName.Contains(searchString)
                                    || r.Description.Contains(searchString));
            }

            // Apply capacity filters
            if (minCapacity.HasValue)
            {
                rooms = rooms.Where(r => r.Capacity >= minCapacity.Value);
            }

            if (maxCapacity.HasValue)
            {
                rooms = rooms.Where(r => r.Capacity <= maxCapacity.Value);
            }

            // Apply price filters
            if (minPrice.HasValue)
            {
                rooms = rooms.Where(r => r.PricePerNight >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                rooms = rooms.Where(r => r.PricePerNight <= maxPrice.Value);
            }

            // Apply availability filter
            if (isAvailable.HasValue)
            {
                rooms = rooms.Where(r => r.IsAvailable == isAvailable.Value);
            }

            // Apply sorting
            switch (sortOrder)
            {
                case "name_desc":
                    rooms = rooms.OrderByDescending(r => r.RoomName);
                    break;
                case "price":
                    rooms = rooms.OrderBy(r => r.PricePerNight);
                    break;
                case "price_desc":
                    rooms = rooms.OrderByDescending(r => r.PricePerNight);
                    break;
                case "capacity":
                    rooms = rooms.OrderBy(r => r.Capacity);
                    break;
                case "capacity_desc":
                    rooms = rooms.OrderByDescending(r => r.Capacity);
                    break;
                case "roomnumber":
                    rooms = rooms.OrderBy(r => r.Roomumber);
                    break;
                case "roomnumber_desc":
                    rooms = rooms.OrderByDescending(r => r.Roomumber);
                    break;
                default:
                    rooms = rooms.OrderBy(r => r.RoomName);
                    break;
            }

            return View(await rooms.ToListAsync());
        }

        // GET: Rooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Room
                .FirstOrDefaultAsync(m => m.RoomId == id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // GET: Rooms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rooms/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoomId,RoomName,Roomumber,Capacity,Description,PricePerNight,RoomSize,IsAvailable")] Room room)
        {
            if (ModelState.IsValid)
            {
                _context.Add(room);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(room);
        }

        // GET: Rooms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Room.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }
            return View(room);
        }

        // POST: Rooms/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RoomId,RoomName,Roomumber,Capacity,Description,PricePerNight,RoomSize,IsAvailable")] Room room)
        {
            if (id != room.RoomId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(room);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomExists(room.RoomId))
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
            return View(room);
        }

        // GET: Rooms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Room
                .FirstOrDefaultAsync(m => m.RoomId == id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // POST: Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var room = await _context.Room.FindAsync(id);
            if (room != null)
            {
                _context.Room.Remove(room);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoomExists(int id)
        {
            return _context.Room.Any(e => e.RoomId == id);
        }
    }
}
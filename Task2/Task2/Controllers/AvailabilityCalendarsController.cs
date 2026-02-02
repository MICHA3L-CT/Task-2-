using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Task2.Data;
using Task2.Models;

namespace Task2.Controllers
{
    public class AvailabilityCalendarsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AvailabilityCalendarsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AvailabilityCalendars
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.AvailabilityCalendar.Include(a => a.Room);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: AvailabilityCalendars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var availabilityCalendar = await _context.AvailabilityCalendar
                .Include(a => a.Room)
                .FirstOrDefaultAsync(m => m.AvailabilityCalendarId == id);
            if (availabilityCalendar == null)
            {
                return NotFound();
            }

            return View(availabilityCalendar);
        }

        // GET: AvailabilityCalendars/Create
        public IActionResult Create()
        {
            ViewData["RoomId"] = new SelectList(_context.Set<Room>(), "RoomId", "RoomId");
            return View();
        }

        // POST: AvailabilityCalendars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AvailabilityCalendarId,RoomId,Date")] AvailabilityCalendar availabilityCalendar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(availabilityCalendar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoomId"] = new SelectList(_context.Set<Room>(), "RoomId", "RoomId", availabilityCalendar.RoomId);
            return View(availabilityCalendar);
        }

        // GET: AvailabilityCalendars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var availabilityCalendar = await _context.AvailabilityCalendar.FindAsync(id);
            if (availabilityCalendar == null)
            {
                return NotFound();
            }
            ViewData["RoomId"] = new SelectList(_context.Set<Room>(), "RoomId", "RoomId", availabilityCalendar.RoomId);
            return View(availabilityCalendar);
        }

        // POST: AvailabilityCalendars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AvailabilityCalendarId,RoomId,Date")] AvailabilityCalendar availabilityCalendar)
        {
            if (id != availabilityCalendar.AvailabilityCalendarId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(availabilityCalendar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AvailabilityCalendarExists(availabilityCalendar.AvailabilityCalendarId))
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
            ViewData["RoomId"] = new SelectList(_context.Set<Room>(), "RoomId", "RoomId", availabilityCalendar.RoomId);
            return View(availabilityCalendar);
        }

        // GET: AvailabilityCalendars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var availabilityCalendar = await _context.AvailabilityCalendar
                .Include(a => a.Room)
                .FirstOrDefaultAsync(m => m.AvailabilityCalendarId == id);
            if (availabilityCalendar == null)
            {
                return NotFound();
            }

            return View(availabilityCalendar);
        }

        // POST: AvailabilityCalendars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var availabilityCalendar = await _context.AvailabilityCalendar.FindAsync(id);
            if (availabilityCalendar != null)
            {
                _context.AvailabilityCalendar.Remove(availabilityCalendar);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AvailabilityCalendarExists(int id)
        {
            return _context.AvailabilityCalendar.Any(e => e.AvailabilityCalendarId == id);
        }
    }
}

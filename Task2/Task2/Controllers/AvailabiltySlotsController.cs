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
    public class AvailabiltySlotsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AvailabiltySlotsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AvailabiltySlots
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.AvailabiltySlots.Include(a => a.AvailabilityCalendar);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: AvailabiltySlots/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var availabiltySlots = await _context.AvailabiltySlots
                .Include(a => a.AvailabilityCalendar)
                .FirstOrDefaultAsync(m => m.AvailabiltySlotsId == id);
            if (availabiltySlots == null)
            {
                return NotFound();
            }

            return View(availabiltySlots);
        }

        // GET: AvailabiltySlots/Create
        public IActionResult Create()
        {
            ViewData["AvailabilityCalendarId"] = new SelectList(_context.AvailabilityCalendar, "AvailabilityCalendarId", "AvailabilityCalendarId");
            return View();
        }

        // POST: AvailabiltySlots/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AvailabiltySlotsId,AvailabilityCalendarId,StartTime,EndTime,IsAvailable")] AvailabiltySlots availabiltySlots)
        {
            if (ModelState.IsValid)
            {
                _context.Add(availabiltySlots);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AvailabilityCalendarId"] = new SelectList(_context.AvailabilityCalendar, "AvailabilityCalendarId", "AvailabilityCalendarId", availabiltySlots.AvailabilityCalendarId);
            return View(availabiltySlots);
        }

        // GET: AvailabiltySlots/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var availabiltySlots = await _context.AvailabiltySlots.FindAsync(id);
            if (availabiltySlots == null)
            {
                return NotFound();
            }
            ViewData["AvailabilityCalendarId"] = new SelectList(_context.AvailabilityCalendar, "AvailabilityCalendarId", "AvailabilityCalendarId", availabiltySlots.AvailabilityCalendarId);
            return View(availabiltySlots);
        }

        // POST: AvailabiltySlots/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AvailabiltySlotsId,AvailabilityCalendarId,StartTime,EndTime,IsAvailable")] AvailabiltySlots availabiltySlots)
        {
            if (id != availabiltySlots.AvailabiltySlotsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(availabiltySlots);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AvailabiltySlotsExists(availabiltySlots.AvailabiltySlotsId))
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
            ViewData["AvailabilityCalendarId"] = new SelectList(_context.AvailabilityCalendar, "AvailabilityCalendarId", "AvailabilityCalendarId", availabiltySlots.AvailabilityCalendarId);
            return View(availabiltySlots);
        }

        // GET: AvailabiltySlots/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var availabiltySlots = await _context.AvailabiltySlots
                .Include(a => a.AvailabilityCalendar)
                .FirstOrDefaultAsync(m => m.AvailabiltySlotsId == id);
            if (availabiltySlots == null)
            {
                return NotFound();
            }

            return View(availabiltySlots);
        }

        // POST: AvailabiltySlots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var availabiltySlots = await _context.AvailabiltySlots.FindAsync(id);
            if (availabiltySlots != null)
            {
                _context.AvailabiltySlots.Remove(availabiltySlots);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AvailabiltySlotsExists(int id)
        {
            return _context.AvailabiltySlots.Any(e => e.AvailabiltySlotsId == id);
        }
    }
}

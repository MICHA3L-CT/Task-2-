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
    public class BookingStatusHistoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingStatusHistoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BookingStatusHistories
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.BookingStatusHistory.Include(b => b.Booking);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: BookingStatusHistories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookingStatusHistory = await _context.BookingStatusHistory
                .Include(b => b.Booking)
                .FirstOrDefaultAsync(m => m.BookingStatusHistoryId == id);
            if (bookingStatusHistory == null)
            {
                return NotFound();
            }

            return View(bookingStatusHistory);
        }

        // GET: BookingStatusHistories/Create
        public IActionResult Create()
        {
            ViewData["BookingId"] = new SelectList(_context.Booking, "BookingId", "BookingId");
            return View();
        }

        // POST: BookingStatusHistories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookingStatusHistoryId,BookingId,OldStatus,NewStatus,ChangedAt")] BookingStatusHistory bookingStatusHistory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookingStatusHistory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookingId"] = new SelectList(_context.Booking, "BookingId", "BookingId", bookingStatusHistory.BookingId);
            return View(bookingStatusHistory);
        }

        // GET: BookingStatusHistories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookingStatusHistory = await _context.BookingStatusHistory.FindAsync(id);
            if (bookingStatusHistory == null)
            {
                return NotFound();
            }
            ViewData["BookingId"] = new SelectList(_context.Booking, "BookingId", "BookingId", bookingStatusHistory.BookingId);
            return View(bookingStatusHistory);
        }

        // POST: BookingStatusHistories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookingStatusHistoryId,BookingId,OldStatus,NewStatus,ChangedAt")] BookingStatusHistory bookingStatusHistory)
        {
            if (id != bookingStatusHistory.BookingStatusHistoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookingStatusHistory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingStatusHistoryExists(bookingStatusHistory.BookingStatusHistoryId))
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
            ViewData["BookingId"] = new SelectList(_context.Booking, "BookingId", "BookingId", bookingStatusHistory.BookingId);
            return View(bookingStatusHistory);
        }

        // GET: BookingStatusHistories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookingStatusHistory = await _context.BookingStatusHistory
                .Include(b => b.Booking)
                .FirstOrDefaultAsync(m => m.BookingStatusHistoryId == id);
            if (bookingStatusHistory == null)
            {
                return NotFound();
            }

            return View(bookingStatusHistory);
        }

        // POST: BookingStatusHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookingStatusHistory = await _context.BookingStatusHistory.FindAsync(id);
            if (bookingStatusHistory != null)
            {
                _context.BookingStatusHistory.Remove(bookingStatusHistory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingStatusHistoryExists(int id)
        {
            return _context.BookingStatusHistory.Any(e => e.BookingStatusHistoryId == id);
        }
    }
}

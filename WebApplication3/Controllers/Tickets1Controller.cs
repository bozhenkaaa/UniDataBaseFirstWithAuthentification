using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class Tickets1Controller : Controller
    {
        private readonly Lab1Context _context;

        public Tickets1Controller(Lab1Context context)
        {
            _context = context;
        }

        // GET: Tickets1
        public async Task<IActionResult> Index(int?id, DateTime? name)
        {
            if (id == null) return RedirectToAction("Index", "TrainSchedules");
            ViewBag.PId = id;
            ViewBag.TrainDate = name;
            //var lab1Context = _context.Trains.Include(t => t.Schedule);
            var tickets = _context.Tickets.Where(b => b.PId == id).Include(b => b.Ps).Include(b => b.Carriage).Include(b => b.PIdNavigation);
            return View(await tickets.ToListAsync());
        }

        // GET: Tickets1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tickets == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .Include(t => t.Carriage)
                .Include(t => t.PIdNavigation)
                .Include(t => t.Ps)
                .FirstOrDefaultAsync(m => m.TicketId == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // GET: Tickets1/Create
        public IActionResult Create()
        {
            ViewData["CarriageId"] = new SelectList(_context.Carriages, "CarriageId", "CarriageName");
            ViewData["PId"] = new SelectList(_context.TrainSchedules, "PId", "PId");
            ViewData["PsId"] = new SelectList(_context.Passengers, "PsId", "PsName");
            return View();
        }

        // POST: Tickets1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TicketId,TicketPrice,PsId,PId,CarriageId,DateOfBuying,PlaceNumber")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ticket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarriageId"] = new SelectList(_context.Carriages, "CarriageId", "CarriageName", ticket.CarriageId);
            ViewData["PId"] = new SelectList(_context.TrainSchedules, "PId", "PId", ticket.PId);
            ViewData["PsId"] = new SelectList(_context.Passengers, "PsId", "PsName", ticket.PsId);
            return View(ticket);
        }

        // GET: Tickets1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tickets == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            ViewData["CarriageId"] = new SelectList(_context.Carriages, "CarriageId", "CarriageName", ticket.CarriageId);
            ViewData["PId"] = new SelectList(_context.TrainSchedules, "PId", "PId", ticket.PId);
            ViewData["PsId"] = new SelectList(_context.Passengers, "PsId", "PsName", ticket.PsId);
            return View(ticket);
        }

        // POST: Tickets1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TicketId,TicketPrice,PsId,PId,CarriageId,DateOfBuying,PlaceNumber")] Ticket ticket)
        {
            if (id != ticket.TicketId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(ticket.TicketId))
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
            ViewData["CarriageId"] = new SelectList(_context.Carriages, "CarriageId", "CarriageName", ticket.CarriageId);
            ViewData["PId"] = new SelectList(_context.TrainSchedules, "PId", "PId", ticket.PId);
            ViewData["PsId"] = new SelectList(_context.Passengers, "PsId", "PsName", ticket.PsId);
            return View(ticket);
        }

        // GET: Tickets1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tickets == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .Include(t => t.Carriage)
                .Include(t => t.PIdNavigation)
                .Include(t => t.Ps)
                .FirstOrDefaultAsync(m => m.TicketId == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: Tickets1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tickets == null)
            {
                return Problem("Entity set 'Lab1Context.Tickets'  is null.");
            }
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket != null)
            {
                _context.Tickets.Remove(ticket);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketExists(int id)
        {
          return _context.Tickets.Any(e => e.TicketId == id);
        }
    }
}

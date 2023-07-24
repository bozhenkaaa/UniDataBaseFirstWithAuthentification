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
    public class Passengers1Controller : Controller
    {
        private readonly Lab1Context _context;

        public Passengers1Controller(Lab1Context context)
        {
            _context = context;
        }

        // GET: Passengers1
        public async Task<IActionResult> Index()
        {
              return View(await _context.Passengers.ToListAsync());
        }

        // GET: Passengers1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Passengers == null)
            {
                return NotFound();
            }

            var passenger = await _context.Passengers
                .FirstOrDefaultAsync(m => m.PsId == id);
            if (passenger == null)
            {
                return NotFound();
            }

            return View(passenger);
        }

        // GET: Passengers1/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Passengers1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PsId,PsSurname,PsName,PsPhone,PsPassport,PsEmail")] Passenger passenger)
        {
            if (ModelState.IsValid)
            {
                _context.Add(passenger);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(passenger);
        }

        // GET: Passengers1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Passengers == null)
            {
                return NotFound();
            }

            var passenger = await _context.Passengers.FindAsync(id);
            if (passenger == null)
            {
                return NotFound();
            }
            return View(passenger);
        }

        // POST: Passengers1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PsId,PsSurname,PsName,PsPhone,PsPassport,PsEmail")] Passenger passenger)
        {
            if (id != passenger.PsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(passenger);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PassengerExists(passenger.PsId))
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
            return View(passenger);
        }

        // GET: Passengers1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Passengers == null)
            {
                return NotFound();
            }

            var passenger = await _context.Passengers
                .FirstOrDefaultAsync(m => m.PsId == id);
            if (passenger == null)
            {
                return NotFound();
            }

            return View(passenger);
        }

        // POST: Passengers1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Passengers == null)
            {
                return Problem("Entity set 'Lab1Context.Passengers'  is null.");
            }
            var passenger = await _context.Passengers.FindAsync(id);
            if (passenger != null)
            {
                _context.Passengers.Remove(passenger);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PassengerExists(int id)
        {
          return _context.Passengers.Any(e => e.PsId == id);
        }
    }
}

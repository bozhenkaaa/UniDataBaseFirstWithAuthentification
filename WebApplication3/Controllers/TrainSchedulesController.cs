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
    public class TrainSchedulesController : Controller
    {
        private readonly Lab1Context _context;

        public TrainSchedulesController(Lab1Context context)
        {
            _context = context;
        }

        // GET: TrainSchedules
        public async Task<IActionResult> Index(int? id, string? name)
        {
            if (id == null) return RedirectToAction("Index", "Trains1");
            ViewBag.TrainId = id;
            ViewBag.TrainDestination = name;
            //var lab1Context = _context.Trains.Include(t => t.Schedule);
            var trainsBySchedule = _context.TrainSchedules.Where(b => b.TrainId == id).Include(b => b.Train);
            return View(await trainsBySchedule.ToListAsync());
        }

        // GET: TrainSchedules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TrainSchedules == null)
            {
                return NotFound();
            }

            var trainSchedule = await _context.TrainSchedules
                .Include(t => t.Train)
                .FirstOrDefaultAsync(m => m.PId == id);
            if (trainSchedule == null)
            {
                return NotFound();
            }

            return RedirectToAction("Index", "Tickets1", new { id = trainSchedule.PId, name = trainSchedule.TrainDate });
        }

        // GET: TrainSchedules/Create
        public IActionResult Create()
        {
            ViewData["TrainId"] = new SelectList(_context.Trains, "TrainId", "TrainDeparture");
            return View();
        }

        // POST: TrainSchedules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PId,TrainId,TrainDate")] TrainSchedule trainSchedule)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trainSchedule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TrainId"] = new SelectList(_context.Trains, "TrainId", "TrainDeparture", trainSchedule.TrainId);
            return View(trainSchedule);
        }

        // GET: TrainSchedules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TrainSchedules == null)
            {
                return NotFound();
            }

            var trainSchedule = await _context.TrainSchedules.FindAsync(id);
            if (trainSchedule == null)
            {
                return NotFound();
            }
            ViewData["TrainId"] = new SelectList(_context.Trains, "TrainId", "TrainDeparture", trainSchedule.TrainId);
            return View(trainSchedule);
        }

        // POST: TrainSchedules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PId,TrainId,TrainDate")] TrainSchedule trainSchedule)
        {
            if (id != trainSchedule.PId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trainSchedule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainScheduleExists(trainSchedule.PId))
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
            ViewData["TrainId"] = new SelectList(_context.Trains, "TrainId", "TrainDeparture", trainSchedule.TrainId);
            return View(trainSchedule);
        }

        // GET: TrainSchedules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TrainSchedules == null)
            {
                return NotFound();
            }

            var trainSchedule = await _context.TrainSchedules
                .Include(t => t.Train)
                .FirstOrDefaultAsync(m => m.PId == id);
            if (trainSchedule == null)
            {
                return NotFound();
            }

            return View(trainSchedule);
        }

        // POST: TrainSchedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TrainSchedules == null)
            {
                return Problem("Entity set 'Lab1Context.TrainSchedules'  is null.");
            }
            var trainSchedule = await _context.TrainSchedules.FindAsync(id);
            if (trainSchedule != null)
            {
                _context.TrainSchedules.Remove(trainSchedule);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrainScheduleExists(int id)
        {
          return _context.TrainSchedules.Any(e => e.PId == id);
        }
    }
}

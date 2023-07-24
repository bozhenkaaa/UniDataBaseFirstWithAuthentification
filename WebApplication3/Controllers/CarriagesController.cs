using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Models;


namespace WebApplication3.Controllers
{
    public class CarriagesController : Controller
    {
        private readonly Lab1Context _context;

        public CarriagesController(Lab1Context context)
        {
            _context = context;
        }

        // GET: Carriages
        public async Task<IActionResult> Index(int? id, string? name)
        {
            if (id == null) return RedirectToAction("Index", "Trains");
            ViewBag.TrainId = id;
            ViewBag.TrainDestination = name;
            //var lab1Context = _context.Trains.Include(t => t.Schedule);
            var carriages = _context.Carriages.Where(b => b.TrainId == id).Include(b => b.Train);
            return View(await carriages.ToListAsync());
           // var lab1Context = _context.Carriages.Include(c => c.Train);
           // return View(await lab1Context.ToListAsync());
        }

        // GET: Carriages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Carriages == null)
            {
                return NotFound();
            }

            var carriage = await _context.Carriages
                .Include(c => c.Train)
                .FirstOrDefaultAsync(m => m.CarriageId == id);
            if (carriage == null)
            {
                return NotFound();
            }

            return RedirectToAction("Index", "Tickets2", new { id = carriage.CarriageId, name = carriage.CarriageName });
        }

        // GET: Carriages/Create
        //public IActionResult Create()
        //{
        //ViewData["TrainId"] = new SelectList(_context.Trains, "TrainId", "TrainDeparture");
        //return View();
        //}
        
        public IActionResult Create(int Id)
        {
            //ViewData["ScheduleId"] = new SelectList(_context.Schedules, "ScheduleId", "ScheduleId");
            ViewBag.TrainId = Id;
            ViewBag.TrainDeparture = _context.Trains.Where(c => c.TrainId == Id).FirstOrDefault().TrainDeparture;
            return View();
        }

        // POST: Carriages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int Id,[Bind("CarriageId,TrainId,CarriageType,CarriageName,PlaceCount")] Carriage carriage)
        {
            carriage.TrainId = Id;
            if (ModelState.IsValid)
            {
                _context.Add(carriage);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Carriages", new { id = Id, name = _context.Trains.Where(c => c.TrainId == Id).FirstOrDefault().TrainDestination });
            }
            ViewData["TrainId"] = new SelectList(_context.Trains, "TrainId", "TrainDeparture", carriage.TrainId);
            return RedirectToAction("Index", "Carriages", new { id = Id, name = _context.Trains.Where(c => c.TrainId == Id).FirstOrDefault().TrainDestination });
        }

        // GET: Carriages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Carriages == null)
            {
                return NotFound();
            }

            var carriage = await _context.Carriages.FindAsync(id);
            if (carriage == null)
            {
                return NotFound();
            }
            ViewData["TrainId"] = new SelectList(_context.Trains, "TrainId", "TrainDeparture", carriage.TrainId);
            return View(carriage);
        }

        // POST: Carriages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CarriageId,TrainId,CarriageType,CarriageName,PlaceCount")] Carriage carriage)
        {
            if (id != carriage.CarriageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carriage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarriageExists(carriage.CarriageId))
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
            ViewData["TrainId"] = new SelectList(_context.Trains, "TrainId", "TrainDeparture", carriage.TrainId);
            return View(carriage);
        }

        // GET: Carriages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Carriages == null)
            {
                return NotFound();
            }

            var carriage = await _context.Carriages
                .Include(c => c.Train)
                .FirstOrDefaultAsync(m => m.CarriageId == id);
            if (carriage == null)
            {
                return NotFound();
            }

            return View(carriage);
        }

        // POST: Carriages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Carriages == null)
            {
                return Problem("Entity set 'Lab1Context.Carriages'  is null.");
            }
            var carriage = await _context.Carriages.FindAsync(id);
            if (carriage != null)
            {
                _context.Carriages.Remove(carriage);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarriageExists(int id)
        {
          return _context.Carriages.Any(e => e.CarriageId == id);
        }
    }
}

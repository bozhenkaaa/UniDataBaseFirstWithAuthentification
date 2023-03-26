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
    public class TrainsController : Controller
    {
        private readonly Lab1Context _context;

        public TrainsController(Lab1Context context)
        {
            _context = context;
        }

        // GET: Trains
        public async Task<IActionResult> Index(int? id, string? name)
        {
            if(id==null) return RedirectToAction("Index", "Schedules");
            ViewBag.ScheduleId = id;
            ViewBag.ScheduleName = name;
            //var lab1Context = _context.Trains.Include(t => t.Schedule);
            var trainsBySchedule = _context.Trains.Where(b => b.ScheduleId == id).Include(b => b.Schedule);
            return View(await trainsBySchedule.ToListAsync());
        }

        // GET: Trains/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Trains == null)
            {
                return NotFound();
            }

            var train = await _context.Trains
                .Include(t => t.Schedule)
                .FirstOrDefaultAsync(m => m.TrainId == id);
            if (train == null)
            {
                return NotFound();
            }

            //return View(train);
            return RedirectToAction("Index", "Carriages", new { id = train.TrainId, name = train.TrainDestination });
        }

        // GET: Trains/Create
        public IActionResult Create(int Id)
        {
            //ViewData["ScheduleId"] = new SelectList(_context.Schedules, "ScheduleId", "ScheduleId");
            ViewBag.ScheduleId = Id;
            ViewBag.StationName = _context.Schedules.Where(c => c.ScheduleId == Id).FirstOrDefault().StationName;
            return View();
        }

        // POST: Trains/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int Id,[Bind("TrainId,TrainDeparture,TrainDestination,TrainTimeOfDep,TrainTimeOfStop,TrainType,ScheduleId")] Train train)
        {
            train.ScheduleId = Id;
            if (ModelState.IsValid)
            {
                _context.Add(train);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index","Trains", new {id=Id, name= _context.Schedules.Where(c => c.ScheduleId == Id).FirstOrDefault().StationName });
            }
            ViewData["ScheduleId"] = new SelectList(_context.Schedules, "ScheduleId", "ScheduleId", train.ScheduleId);
            return RedirectToAction("Index", "Trains", new { id = Id, name = _context.Schedules.Where(c => c.ScheduleId == Id).FirstOrDefault().StationName });
        }

        // GET: Trains/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Trains == null)
            {
                return NotFound();
            }

            var train = await _context.Trains.FindAsync(id);
            if (train == null)
            {
                return NotFound();
            }
            ViewData["ScheduleId"] = new SelectList(_context.Schedules, "ScheduleId", "ScheduleId", train.ScheduleId);

            return View(train);
        }

        // POST: Trains/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TrainId,TrainDeparture,TrainDestination,TrainTimeOfDep,TrainTimeOfStop,TrainType,ScheduleId")] Train train)
        {
            if (id != train.TrainId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(train);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainExists(train.TrainId))
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
            ViewData["ScheduleId"] = new SelectList(_context.Schedules, "ScheduleId", "ScheduleId", train.ScheduleId);
            return View(train);
        }

        // GET: Trains/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Trains == null)
            {
                return NotFound();
            }

            var train = await _context.Trains
                .Include(t => t.Schedule)
                .FirstOrDefaultAsync(m => m.TrainId == id);
            if (train == null)
            {
                return NotFound();
            }

            return View(train);
            
        }

        // POST: Trains/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Trains == null)
            {
                return Problem("Entity set 'Lab1Context.Trains'  is null.");
            }
            var train = await _context.Trains.FindAsync(id);
            if (train != null)
            {
                _context.Trains.Remove(train);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrainExists(int id)
        {
          return (_context.Trains?.Any(e => e.TrainId == id)).GetValueOrDefault();
        }
    }
}

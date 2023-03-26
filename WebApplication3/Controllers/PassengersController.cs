using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class PassengersController : Controller
    {
        private readonly Lab1Context _context;

        public PassengersController(Lab1Context context)
        {
            _context = context;
        }

        // GET: Passengers
        public async Task<IActionResult> Index()
        {
              return View(await _context.Passengers.ToListAsync());
        }

        // GET: Passengers/Details/5
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

            return RedirectToAction("Index", "Tickets", new { id = passenger.PsId, name = passenger.PsSurname });
        }

        // GET: Passengers/Create
        public IActionResult Create(int id)
        {
            //ViewBag.PsId = id;
            //ViewBag.PsSurname = _context.Passengers.Where(c => c.TrainId == Id).FirstOrDefault().TrainDeparture;
            return View();
        }

        // POST: Passengers/Create
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

        // GET: Passengers/Edit/5
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

        // POST: Passengers/Edit/5
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

        // GET: Passengers/Delete/5
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

        // POST: Passengers/Delete/5
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
        public FileResult Export()
        {
            System.Console.InputEncoding = Encoding.GetEncoding(1251);
           
            var categories = _context.Passengers.ToList();
            string word = string.Empty;
            word = "PsId" +" , "+ "PsPhone" + " , " + "PsPassport" + " , " + "PsEmail";
            word += "\r\n";
            for (int i = 0; i < categories.Count; i++)
            {
                word += categories[i].PsId.ToString().Replace(",", ";") + ",";
                word += categories[i].PsPhone.ToString().Replace(",", ";") + ",";
                word += categories[i].PsPassport.ToString().Replace(",", ";") + ",";
                if (categories[i].PsEmail != null)
                {
                    word += categories[i].PsEmail.Replace(",", ";") + ",";
                }
                word += "\r\n";
            }
            byte[] bytes = Encoding.ASCII.GetBytes(word);
            return File(bytes, "application/vnd.ms-word", "Pass.doc");
        }
    }
}

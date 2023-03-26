using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartController : ControllerBase
    {
        private readonly Lab1Context _context;
        public ChartController(Lab1Context context)
        {
            _context = context;
        }
        [HttpGet("JsonData")]
        public JsonResult JsonData()
        {
            var schedules = _context.Schedules.Include(a => a.Trains).ToList();
            List<object> schedule = new List<object>();
            schedule.Add(new[] { "Розклад", "Кількість потягів" });
            foreach(var c in schedules)
            {
                schedule.Add(new object[] { c.StationName, c.Trains.Count() });
            }
            return new JsonResult(schedule);
        }
        [HttpGet("JsonData1")]
        public JsonResult JsonData1()
        {
            var pas = _context.Passengers.Include(a => a.Tickets).ToList();
            List<object> p = new List<object>();
            p.Add(new[] { "Пасажир", "Кількість квитків" });
            foreach (var c in pas)
            {
                p.Add(new object[] { c.PsSurname, c.Tickets.Count() });
            }
            return new JsonResult(p);
        }
        [HttpGet("JsonData2")]
        public JsonResult JsonData2()
        {
            var ts = _context.Trains.Include(a => a.TrainSchedules).ToList();
            List<object> p = new List<object>();
            p.Add(new[] { "Потяг","Кількість відправлень" });
            foreach (var c in ts)
            {
                p.Add(new object[] { c.TrainDeparture,  c.TrainSchedules.Count() });
            }
            return new JsonResult(p);
        }

    }
}

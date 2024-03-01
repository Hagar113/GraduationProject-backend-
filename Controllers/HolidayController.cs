using GraduationProject.DTOs;
using GraduationProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HolidayController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public HolidayController(ApplicationDbContext context)
        {
            _context = context;
        }
        

        [HttpGet]
        public ActionResult GetHolidays()
        {
            try
            {
                List<Holiday> holidays = _context.Holidays.ToList();

                return Ok(holidays);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteHoliday(int id)
        {
            try
            {
              
                var holidayToDelete = _context.Holidays.FirstOrDefault(h => h.Id == id);

                if (holidayToDelete == null)
                {
                    return NotFound();
                }

                _context.Holidays.Remove(holidayToDelete);
                _context.SaveChanges();

                return Ok(); 
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public ActionResult UpdateHoliday(HolidayReq holidayReq)
        {
            try
            {
                var AyAgaza = _context.Holidays.Find(holidayReq.id);

                if (AyAgaza == null)
                {
                    return NotFound();
                }

                AyAgaza.Name = holidayReq.Name;
                AyAgaza.Date = holidayReq.Date.Value;
              
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult AddHoliday(HolidayReq holidayReq)
        {
            try
            {
                Holiday holiday = new Holiday();

                holiday.Name = holidayReq.Name;
                holiday.Date = holidayReq.Date.Value;

                _context.Add(holiday);
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

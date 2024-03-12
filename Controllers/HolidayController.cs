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
                List<HolidayReq> holidayDtos = new List<HolidayReq>();
                foreach (var holiday in holidays)
                {
                    holidayDtos.Add(new HolidayReq
                    {
                        id = holiday.Id,
                        name = holiday.Name,
                        date = holiday.Date,
                    });
                }
                return Ok(holidayDtos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("DeleteHoliday")]
        public ActionResult DeleteHoliday([FromBody] int id)
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

        [HttpPost("UpdateHoliday")]
        public ActionResult UpdateHoliday([FromBody] HolidayReq holidayReq)
        {
            try
            {
                var holiday = _context.Holidays.Find(holidayReq.id);

                if (holiday == null)
                {
                    return NotFound();
                }

                holiday.Name = holidayReq.name;
                holiday.Date = holidayReq.date.Value;

                _context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddHoliday")]
        public ActionResult AddHoliday([FromBody] HolidayReq holidayReq)
        {
            try
            {
                Holiday holiday = new Holiday();

                holiday.Name = holidayReq.name;
                holiday.Date = holidayReq.date.Value;

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

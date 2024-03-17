using GraduationProject.DTOs;
using GraduationProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace GraduationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneralSettingsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public GeneralSettingsController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpPost]
        public ActionResult SaveSettings(GeneralSettingDTO generalSettingDTO)
        {
            GeneralSettings general = new GeneralSettings()
            {
                Addition = generalSettingDTO.Addition,
                Deduction = generalSettingDTO.Deduction,
                Method=generalSettingDTO.Method,
                SelectedFirstWeekendDay = generalSettingDTO.SelectedFirstWeekendDay,
                SelectedSecondWeekendDay = generalSettingDTO.SelectedSecondWeekendDay
            };

            _context.generalSettings.Add(general);
            _context.SaveChanges();

            return Ok();
        }




        [HttpGet]
        public ActionResult GetGeneralSettingsList()
        {
            GeneralSettings settingsList = _context.generalSettings.OrderByDescending(z => z.Id).FirstOrDefault();
            if (settingsList == null) // Check if list is empty
            {
                return NotFound(); // Return not found status if no settings exist
            }
            return Ok(settingsList);
        }

    }

}



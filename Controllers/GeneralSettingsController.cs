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
                SelectedFirstWeekendDay = generalSettingDTO.SelectedFirstWeekendDay,
                SelectedSecondWeekendDay = generalSettingDTO.SelectedSecondWeekendDay
            };

            _context.generalSettings.Add(general);
            _context.SaveChanges();

            return Ok();
        }



        //[HttpGet]
        //public ActionResult GetGeneralSettingsList()
        //{
        //    List<GeneralSettings> settingsList = _context.generalSettings.ToList();
        //    if (!settingsList.Any()) // Check if list is empty
        //    {
        //        return NotFound(); // Return not found status if no settings exist
        //    }
        //    return Ok(settingsList);
        //}



        [HttpGet]
        public ActionResult<GeneralSettings> GetWeekendDays()
        {


            GeneralSettings generalSettings = _context.generalSettings.OrderByDescending(x => x.Id).FirstOrDefault();

            if (generalSettings == null)
            {
                generalSettings = new GeneralSettings
                {
                    SelectedFirstWeekendDay = "Saturday",
                    SelectedSecondWeekendDay = "Sunday",
                    Addition = 0,
                    Deduction = 0
                };
            }

            return Ok(generalSettings);
        }

    }

}



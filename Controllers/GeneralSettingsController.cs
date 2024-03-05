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

        //public int id { get; private set; }

        //[HttpGet]
        //public ActionResult GetGeneralSettings()
        //{
        //    var settings = _context.generalSettings.FirstOrDefault();
        //    if (settings == null)
        //    {
        //        return Ok(new GeneralSettings() { Deduction = 0, Addition = 0, SelectedFirstWeekendDay = "", SelectedSecondWeekendDay = "" });
        //    }
        //    else
        //    {
        //        return Ok(settings);
        //    }
        //}
        //------------------------------imp-------------------------------------------
        //[HttpPost]
        //public ActionResult SaveSettings(GeneralSettingDTO generalSettingDTO)
        //{
        //    var setting = _context.generalSettings.FirstOrDefault(g => (g.Id == generalSettingDTO.id));
        //    if (setting == null)
        //    {
        //        GeneralSettings general = new GeneralSettings()
        //        {
        //            Addition = generalSettingDTO.Addition,
        //            Deduction =generalSettingDTO.Deduction,
        //            SelectedFirstWeekendDay = generalSettingDTO.SelectedFirstWeekendDay,
        //            SelectedSecondWeekendDay = generalSettingDTO.SelectedSecondWeekendDay
        //        };
        //        _context.generalSettings.Add(general);
        //        _context.SaveChanges();
        //        return Ok();
        //    }
        //    else
        //    {
        //        setting.Deduction = generalSettingDTO.Deduction;
        //        setting.Addition = generalSettingDTO.Addition;


        //        setting.SelectedFirstWeekendDay = generalSettingDTO.SelectedFirstWeekendDay;
        //        setting.SelectedSecondWeekendDay =generalSettingDTO.SelectedSecondWeekendDay;
        //        _context.Entry(setting).State = EntityState.Modified;
        //        _context.SaveChanges();
        //        return Ok();
        //    }
        //}
        //----------------------------------------------------------------------------
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

        // ---------------------------------------------------------------------------------

        [HttpGet]
        public ActionResult GetGeneralSettingsList()
        {
            List<GeneralSettings> settingsList = _context.generalSettings.ToList();
            if (!settingsList.Any()) // Check if list is empty
            {
                return NotFound(); // Return not found status if no settings exist
            }
            return Ok(settingsList);
        }
        // --------z------------------------------------------------------------------------
        //[HttpGet]
        //public ActionResult GetGeneralSettings()
        //{
        //    GeneralSettings settings = _context.generalSettings.FirstOrDefault();
        //    if (settings == null)
        //    {
        //        return Ok(new GeneralSettings() { Deduction = 0, Addition = 0, SelectedFirstWeekendDay = "", SelectedSecondWeekendDay = "" });
        //    }
        //    else
        //    {
        //        return Ok(settings);
        //    }
        //}

        //[HttpPost]
        //public ActionResult SaveSettings(GeneralSettingDTO generalSettingDTO)
        //{
        //    var setting = _context.generalSettings.FirstOrDefault();
        //    if (setting == null)
        //    {
        //        GeneralSettings general = new GeneralSettings()
        //        {
        //            Addition = generalSettingDTO.Addition,
        //            Deduction = generalSettingDTO.Deduction,
        //            SelectedFirstWeekendDay = generalSettingDTO.SelectedFirstWeekendDay,
        //            SelectedSecondWeekendDay = generalSettingDTO.SelectedSecondWeekendDay
        //        };
        //        _context.generalSettings.Add(general);
        //        _context.SaveChanges();
        //        return Ok();
        //    }
        //    else
        //    {
        //        setting.Deduction = generalSettingDTO.Deduction;
        //        setting.Addition = generalSettingDTO.Addition;


        //        setting.SelectedFirstWeekendDay = generalSettingDTO.SelectedFirstWeekendDay;
        //        setting.SelectedSecondWeekendDay = generalSettingDTO.SelectedSecondWeekendDay;
        //        _context.Entry(setting).State = EntityState.Modified;
        //        _context.SaveChanges();
        //        return Ok();
        //    }
        //}


       
        
    }

}



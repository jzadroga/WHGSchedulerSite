using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WHGScheduler.Repository;
using WHGScheduler.Repository.Models;
using WHGSchedulerSite.ViewModels;

namespace WHGSchedulerSite.Controllers
{
    public class MeetingsController : Controller
    {
        public ActionResult Index(int sponsor)
        {
            Meeting.GetListByDay(sponsor);

            return View( new SponsorMeetingsViewModel() {
                sponsor = Sponsor.GetByID(sponsor),
                meetings = Meeting.GetListByDay(sponsor)
            });
        }

        [HttpPost]
        public JsonResult SaveRegistrant(RegistrantModel registrant)
        {
            bool success = true;

            Registrant.Save(registrant);

            return Json(new { Success = success });
        }
    }
}
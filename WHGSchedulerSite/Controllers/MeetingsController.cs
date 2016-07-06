using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WHGScheduler.Repository;
using WHGSchedulerSite.ViewModels;

namespace WHGSchedulerSite.Controllers
{
    public class MeetingsController : Controller
    {
        public ActionResult Index(int sponsor)
        {
            return View( new MeetingsViewModel() {
                sponsor = Sponsor.GetByID(sponsor)
            });
        }
    }
}
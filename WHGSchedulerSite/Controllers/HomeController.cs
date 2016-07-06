using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WHGScheduler.Repository;
using WHGSchedulerSite.ViewModels;

namespace WHGSchedulerSite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View( new SponsorsViewModel() {
                sponsorList = Sponsor.GetList(true)
            });
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WHGScheduler.Repository;
using WHGScheduler.Repository.Models;
using WHGSchedulerSite.ViewModels;

namespace WHGSchedulerSite.Controllers
{
    [Authorize]
    public class ControlPanelController : Controller
    {
        public ActionResult Index()
        {
            return View(new SponsorsViewModel()
            {
                sponsorList = Sponsor.GetList(true)
            });
        }

        [HttpPost]
        public ActionResult SaveSponsor(SponsorModel sponsor, HttpPostedFileBase logoFile)
        {
            if (logoFile != null && logoFile.ContentLength > 0)
            {
                var fileName = Path.GetFileName(logoFile.FileName);
                var path = Path.Combine(Server.MapPath("~/assets/img/logos"), fileName);
                logoFile.SaveAs(path);
                sponsor.logoImage = fileName;
            }

            Sponsor.Save(sponsor);

            return RedirectToAction("Index", "ControlPanel");
        }
    }
}
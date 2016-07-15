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

        [HttpGet]
        public ActionResult DeleteSponsor(int id)
        {
            Sponsor.Delete(id);

            return RedirectToAction("Index", "ControlPanel");
        }

        [HttpGet]
        public ActionResult DeleteMeeting(int id, int sponsor)
        {
            Meeting.Delete(id);

            return RedirectToAction("Meetings", "ControlPanel", new { id = sponsor });
        }

        public ActionResult Registrants(int meetingID)
        {
            return View( new RegistrantsViewModel()
            {
                registrants = Registrant.GetListByMeeting(meetingID),
                meetingID = meetingID,
                meeting = Meeting.GetByID(meetingID)
            });
        }

        public ActionResult DeleteRegistrant(int id, int meetingID)
        {
            Registrant.Delete(id);

            return RedirectToAction("Registrants", "ControlPanel", new { meetingID = meetingID });
        }

        public ActionResult Meetings(int id)
        {
            return View( new MeetingsViewModel()
            {
                sponsor = Sponsor.GetByID(id),
                meetingsList = Meeting.GetList(id, true)
            });
        }

        public ActionResult CreateDefaultMeetings()
        {
            Meeting.CreateDefaultList();

            return RedirectToAction("Index", "ControlPanel");
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

        [HttpPost]
        public ActionResult SaveMeeting(MeetingModel meeting)
        {
            Meeting.Save(meeting);

            return RedirectToAction("Meetings", "ControlPanel", new { id = meeting.sponsorID });
        }
    }
}
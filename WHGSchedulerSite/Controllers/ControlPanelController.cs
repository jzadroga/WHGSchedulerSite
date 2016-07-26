using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
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

        public ActionResult ExportRegistrants(int id)
        {
            var meetingsList = Meeting.GetList(id, true);

            var products = new System.Data.DataTable("registrants");
            products.Columns.Add("Meeting Date", typeof(string));
            products.Columns.Add("Meeting Time", typeof(string));
            products.Columns.Add("First Name", typeof(string));
            products.Columns.Add("Last Name", typeof(string));
            products.Columns.Add("Attendee Type", typeof(string));
            products.Columns.Add("Brand(s)", typeof(string));
            products.Columns.Add("Email", typeof(string));
            products.Columns.Add("Location", typeof(string));
            products.Columns.Add("Business Phone", typeof(string));
            products.Columns.Add("Mobile Phone", typeof(string));
            products.Columns.Add("Please tell us why you would like to speak with us", typeof(string));

            foreach (var meeting in meetingsList)
            {
                var registrants = Registrant.GetListByMeeting(meeting.id);
                foreach (var registrant in registrants)
                {               
                    products.Rows.Add(meeting.startDate.ToShortDateString(), meeting.timeLabel, registrant.firstname, registrant.lastname, registrant.attendeetype, 
                        registrant.brands, registrant.email, registrant.location, registrant.bizphone, registrant.mobilephone, registrant.comments);                 
                }
            }

            var grid = new GridView();
            grid.DataSource = products;
            grid.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=RegistrantList.xls");
            Response.ContentType = "application/ms-excel";

            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            grid.RenderControl(htw);

            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

            return RedirectToAction("Meetings", "ControlPanel", new { id = id });
        }
    }
}
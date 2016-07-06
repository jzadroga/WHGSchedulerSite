using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WHGScheduler.Repository.Models;

namespace WHGSchedulerSite.ViewModels
{
    public class SponsorsViewModel
    {
        public List<SponsorModel> sponsorList { get; set; }

        public SponsorsViewModel()
        {
            this.sponsorList = new List<SponsorModel>();
        }
    }
}
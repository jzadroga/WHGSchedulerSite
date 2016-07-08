using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WHGScheduler.Repository.Models;

namespace WHGSchedulerSite.ViewModels
{
    public class MeetingsViewModel
    {
        public SponsorModel sponsor { get; set; }
        public List<MeetingModel> meetingsList { get; set; }

        public MeetingsViewModel()
        {
            this.sponsor = new SponsorModel();
            this.meetingsList = new List<MeetingModel>(); 
        }
    }
}

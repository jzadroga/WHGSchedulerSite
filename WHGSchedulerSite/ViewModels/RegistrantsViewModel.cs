using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WHGScheduler.Repository.Models;

namespace WHGSchedulerSite.ViewModels
{
    public class RegistrantsViewModel
    {
        public List<RegistrantModel> registrants { get; set; }
        public int meetingID { get; set; }
        public MeetingModel meeting { get; set; }
    }
}

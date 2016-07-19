using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WHGScheduler.Repository.Models
{
    public class SponsorMeetingsModel
    {
        public int day { get; set; }
        public string startDay { get; set; }
        public List<MeetingModel> meetings {get; set;}
    }
}

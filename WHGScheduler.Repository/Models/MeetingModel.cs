using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WHGScheduler.Repository.Models
{
    public class MeetingModel
    {
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public int requests { get; set; }
        public int id { get; set; }
        public int sponsorID { get; set; }
        public string timeLabel { get; set; }
        public bool available { get; set; }
        public string sponserName { get; set; }
    }
}

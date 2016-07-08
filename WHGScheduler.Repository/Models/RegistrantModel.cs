using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WHGScheduler.Repository.Models
{
    public class RegistrantModel
    {
        public int id { get; set; }
        public int sponsorID { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string attendeetype { get; set; }
        public string brands { get; set; }
        public string title { get; set; }
        public string email { get; set; }
        public string location { get; set; }
        public string bizphone { get; set; }
        public string mobilephone { get; set; }
        public string comments { get; set; }
    }
}

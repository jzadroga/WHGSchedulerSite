using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WHGScheduler.Repository.Models
{
    public class SponsorModel
    {
        public string logoImage { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int id { get; set; }
        public string url { get; set; }
        public string email { get; set; }
        public string type { get; set; }
    }
}

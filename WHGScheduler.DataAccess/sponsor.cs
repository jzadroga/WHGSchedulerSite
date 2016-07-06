using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WHGScheduler.DataAccess
{
    public partial class sponsor
    {
        public static List<sponsor> GetList(bool activeOnly)
        {
            WHGSchedulerDBDataContext context = new WHGSchedulerDBDataContext();

            return (activeOnly) ? context.sponsors.Where(sp => sp.status.statusName == "active").ToList() : context.sponsors.ToList(); 
        }
    }
}

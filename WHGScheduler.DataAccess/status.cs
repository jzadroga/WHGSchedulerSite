using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WHGScheduler.DataAccess
{
    public partial class status
    {
        public static int GetActiveStatus(string component)
        {
            WHGSchedulerDBDataContext context = new WHGSchedulerDBDataContext();

            return context.status.Where(st => st.component.componentName == component && st.statusName == "active").FirstOrDefault().statusID;
        }
    }
}

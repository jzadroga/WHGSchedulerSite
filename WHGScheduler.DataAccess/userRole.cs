using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WHGScheduler.DataAccess
{
    public partial class userRole
    {
        public static int GetRoleID(string role)
        {
            WHGSchedulerDBDataContext context = new WHGSchedulerDBDataContext();

            return context.userRoles.Where(r => r.role.ToLower() == role).FirstOrDefault().userRoleID;
        }     
    }
}

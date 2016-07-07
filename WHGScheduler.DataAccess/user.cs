using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WHGScheduler.DataAccess
{
    public partial class user
    {
        public static bool IsValidAdminAccount(string email, string password)
        {
            WHGSchedulerDBDataContext context = new WHGSchedulerDBDataContext();

            var admin = context.users.Where(us => us.email.ToLower() == email.ToLower()
                            && us.password == password
                            && us.userRole.role == "admin"
                            && us.status.statusName == "active").FirstOrDefault();

            return (admin == null) ? false : true; 
        }
    }
}

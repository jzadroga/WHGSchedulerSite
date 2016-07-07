using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WHGScheduler.DataAccess;
using WHGScheduler.Repository.Models;

namespace WHGScheduler.Repository
{
    public class AccountUser
    {
        public static bool IsValidUser(string email, string password, bool adminOnly)
        {
            return user.IsValidAdminAccount(email, password);
        }
    }
}

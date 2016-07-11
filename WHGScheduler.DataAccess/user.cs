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

        public static List<user> GetListByMeeting(int meetingID)
        {
            WHGSchedulerDBDataContext context = new WHGSchedulerDBDataContext();

            return context.users
                        .Where(usr => usr.status.statusName == "active" && usr.userRole.role == "registrant")
                        .Where(usr => usr.meetingRequests.Any(mr => mr.meetingID == meetingID))
                        .ToList();
        }

        public static void SaveRegistrant(user registrant, int meetingID)
        {
            WHGSchedulerDBDataContext context = new WHGSchedulerDBDataContext();
            DateTime current = DateTime.Now;

            //add the new user as a registrant
            registrant.dateCreated = current;
            registrant.statusID = status.GetActiveStatus("user");
            registrant.roleID = userRole.GetRoleID("registrant");

            context.users.InsertOnSubmit(registrant);
            context.SubmitChanges();

            //create the link to the meeting
            meetingRequest request = new meetingRequest();

            request.dateCreated = current;
            request.meetingID = meetingID;
            request.userID = registrant.userID;

            context.meetingRequests.InsertOnSubmit(request);
            context.SubmitChanges();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WHGScheduler.DataAccess
{
    public partial class meeting
    {
        public static List<meeting> GetList(bool activeOnly)
        {
            WHGSchedulerDBDataContext context = new WHGSchedulerDBDataContext();

            return (activeOnly) ? context.meetings.Where(mt => mt.status.statusName == "active").OrderBy( mt => mt.startDate).ToList() : context.meetings.ToList(); 
        }

        public static meeting Get(int id)
        {
            WHGSchedulerDBDataContext context = new WHGSchedulerDBDataContext();

            return context.meetings.Where(mt => mt.meetingID == id).FirstOrDefault();
        }

        public static void Delete(int meetingID)
        {
            WHGSchedulerDBDataContext context = new WHGSchedulerDBDataContext();

            var deleteMeeting = context.meetings.Where(mt => mt.meetingID == meetingID).FirstOrDefault();

            deleteMeeting.statusID = status.GetDeleteStatus("meeting");

            context.SubmitChanges();
        }

        public static void Save(meeting meetingObj)
        {
            WHGSchedulerDBDataContext context = new WHGSchedulerDBDataContext();

            if (meetingObj.meetingID != 0)
            {
                var updateMeeting = context.meetings.Where(mt => mt.meetingID == meetingObj.meetingID).FirstOrDefault();
                if(updateMeeting != null)
                {
                    updateMeeting.startDate = meetingObj.startDate;
                    updateMeeting.endDate = meetingObj.endDate;
                    updateMeeting.availableRequests = meetingObj.availableRequests;
                }
            }
            else
            {
                meetingObj.dateCreated = DateTime.Now;
                meetingObj.statusID = status.GetActiveStatus("meeting");

                context.meetings.InsertOnSubmit(meetingObj);
            }

            context.SubmitChanges();
        }
    }
}

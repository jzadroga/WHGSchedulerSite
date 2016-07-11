using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WHGScheduler.DataAccess
{
    public partial class meeting
    {
        public int registrants
        {
            get
            {
                if (this.meetingRequests == null)
                    return 0;

                return this.meetingRequests.Count();
            }
        }

        public static List<meeting> GetList(int sponsorID, bool activeOnly)
        {
            WHGSchedulerDBDataContext context = new WHGSchedulerDBDataContext();

            return (activeOnly) ? context.meetings.Where(mt => mt.status.statusName == "active" && mt.sponsorID == sponsorID).OrderBy( mt => mt.startDate).ToList() : 
                context.meetings.Where(mt => mt.sponsorID == sponsorID).ToList(); 
        }

        public static List<MeetingsByDay> GetListByDay(int sponsorID)
        {
            WHGSchedulerDBDataContext context = new WHGSchedulerDBDataContext();
            
            return context.meetings
                            .Where( mt => mt.status.statusName == "active" && mt.sponsorID == sponsorID)
                            .OrderByDescending( mt => mt.startDate) 
                            .GroupBy( mt =>mt.startDate.Day ).ToList()
                            .Select(( ds, ind ) => new MeetingsByDay
                            {
                                Day = ind + 1,
                                Meetings = ds.ToList()
                            }).ToList();
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

    public class MeetingsByDay
    {
        public int Day { get; set; }
        public List<meeting> Meetings { get; set; }
    }
}

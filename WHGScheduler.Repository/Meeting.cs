using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WHGScheduler.DataAccess;
using WHGScheduler.Repository.Models;

namespace WHGScheduler.Repository
{
    public class Meeting
    {
        public static List<MeetingModel> GetList(bool activeOnly)
        {
            var meetingList = meeting.GetList(activeOnly);

            return meetingList.Select(mt => new MeetingModel()
            {
                id = mt.meetingID,
                sponsorID = mt.sponsorID,
                startDate = mt.startDate,
                endDate = mt.endDate,
                requests = mt.availableRequests
            }).ToList();
        }

        public static void Save(MeetingModel obj)
        {
            meeting.Save( new meeting(){
                meetingID = obj.id,
                startDate = obj.startDate,
                endDate = obj.endDate,
                sponsorID = obj.sponsorID,
                availableRequests = obj.requests
            });
        }

        public static void Delete(int meetingID)
        {
            meeting.Delete(meetingID);
        }
    }
}

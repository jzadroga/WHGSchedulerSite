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
        public static List<SponsorMeetingsModel> GetListByDay(int sponsorID)
        {
            var dailyMeetings = meeting.GetListByDay(sponsorID);
            return dailyMeetings.Select(smt => new SponsorMeetingsModel()
            {
                day = smt.Day,
                startDay = (smt.Day == 1) ? "September 13" : "September 14",
                meetings = smt.Meetings.Select( mt => new MeetingModel()
                {
                   id = mt.meetingID,
                   startDate = mt.startDate,
                   endDate = mt.endDate,
                   available = (mt.availableRequests > mt.registrants) ? true : false,
                   timeLabel = mt.startDate.ToShortTimeString().ToLower() + " - " + mt.endDate.ToShortTimeString().ToLower()
                }).ToList()
            }).ToList();
        }

        public static List<MeetingModel> GetList(int sponsorID, bool activeOnly)
        {
            var meetingList = meeting.GetList(sponsorID, activeOnly);

            return meetingList.Select(mt => new MeetingModel()
            {
                id = mt.meetingID,
                sponsorID = mt.sponsorID,
                startDate = mt.startDate,
                endDate = mt.endDate,
                requests = mt.availableRequests,
                timeLabel = mt.startDate.ToShortTimeString().ToLower() + " - " + mt.endDate.ToShortTimeString().ToLower()
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

        public static MeetingModel GetByID(int meetingID)
        {
            var meetingObj = meeting.Get(meetingID);
            return new MeetingModel()
            {
                sponsorID = meetingObj.sponsor.sponsorID,
                sponserName = meetingObj.sponsor.name,
                id = meetingObj.meetingID,
                startDate = meetingObj.startDate,
                endDate = meetingObj.endDate,
                available = (meetingObj.availableRequests > meetingObj.registrants) ? true : false,
                timeLabel = meetingObj.startDate.ToShortTimeString().ToLower() + " - " + meetingObj.endDate.ToShortTimeString().ToLower()
            };
        }

        public static void CreateDefaultList()
        {
            meeting.CreateDefaultList();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WHGScheduler.DataAccess;
using WHGScheduler.Repository.Models;

namespace WHGScheduler.Repository
{
    public class Registrant
    {
       
        public static List<RegistrantModel> GetListByMeeting(int meetingID)
        {
            var registrantList = user.GetListByMeeting(meetingID);

            return registrantList.Select(reg => new RegistrantModel()
            {
                id = reg.userID,
                firstname = reg.firstName,
                lastname = reg.lastName,
                email = reg.email,
                attendeetype = reg.attendeeType,
                brands = reg.brands,
                title = reg.title,
                location = reg.location,
                bizphone = reg.businessPhone,
                mobilephone = reg.mobilePhone,
                comments = reg.comments
            }).ToList();
        }

        public static void Save(RegistrantModel obj)
        {
            user.SaveRegistrant( new user(){
                email = obj.email,
                firstName = obj.firstname,
                lastName = obj.lastname,
                attendeeType = obj.attendeetype,
                brands = obj.brands,
                title = obj.title,
                location = obj.location,
                businessPhone = obj.bizphone,
                mobilePhone = obj.mobilephone,
                comments = obj.comments
            }, obj.meetingID);

            //send a confirmation email

        }
    }
}

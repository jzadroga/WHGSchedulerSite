using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using WHGScheduler.DataAccess;
using WHGScheduler.Repository.Models;

namespace WHGScheduler.Repository
{
    public class Registrant
    {
        public static void Delete(int registrantID)
        {
            user.Delete(registrantID);
        }

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
            sendRegistrationEmail(obj);
        }

        private static void sendRegistrationEmail(RegistrantModel registrant)
        {
            try
            {
                var meeting = Meeting.GetByID(registrant.meetingID);

                MailMessage mail = new MailMessage();

                mail.From = new MailAddress("2016whgscheduler@gmail.com", "2016 WHG Scheduler");
                mail.To.Add(new MailAddress(registrant.email));
                mail.CC.Add(new MailAddress("bert@onyxmeetings.com"));

                SmtpClient client = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(mail.From.Address, "@whgscheduler")
                };         

                mail.Subject = "Registration Confirmation";
                mail.Body = "Thank you for scheduling an appointment.  Your meeting time on " + meeting.startDate.ToShortDateString() + " at " + meeting.timeLabel + " with " + meeting.sponserName + " is confirmed.  Your meeting will take place on the Trade Show floor in the sponsor’s booth.To modify or cancel your appointment, please contact XXXXXXXXXX.";

                client.Send(mail);
            }
            catch( Exception ex)
            {
                string failed = ex.Message;
            }
        }
    }
}

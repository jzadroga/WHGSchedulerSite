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
            sendSponsorEmail(obj);
        }

        private static void sendRegistrationEmail(RegistrantModel registrant)
        {
            try
            {
                var meeting = Meeting.GetByID(registrant.meetingID);

                MailMessage mail = new MailMessage();

                mail.From = new MailAddress("2016whgscheduler@gmail.com", "2018 WHG Scheduler");
                mail.To.Add(new MailAddress(registrant.email));
               // mail.CC.Add(new MailAddress("bert@onyxmeetings.com"));

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

                //build out confirm email
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Thank you for scheduling an appointment!");
                sb.AppendLine();
                sb.AppendLine("Your meeting is confirmed for:");
                sb.AppendLine();
                sb.AppendLine(meeting.sponserName);
                sb.AppendLine();
                sb.AppendLine("Date: " + meeting.startDate.ToString("dddd, MMMM dd"));
                sb.AppendLine("Time: " + meeting.timeLabel);
                sb.AppendLine("Location:");
                sb.AppendLine("Supplier Partners: Appointments with supplier partners will take place in that supplier’s booth in Bayside CD at the Mandalay Bay Convention Center. Please refer to the Trade Show Floor Plan for a complete listing of booth numbers.");
                sb.AppendLine();
                sb.AppendLine("Global Village: Appointments with Global Village team members will take place at the Appointment Center in Booth #118 in Bayside CD at the Mandalay Bay Convention Center. Please refer to the Trade Show Floor Plan for the exact location. Please also check in at the Appointment Center Concierge Desk when you arrive and a team member will assist you in finding your meeting contact.");
                sb.AppendLine();
                sb.AppendLine("To modify or cancel your appointment, please contact Bert Guy, bert@onyxmeetings.com.");
                sb.AppendLine();
                sb.AppendLine("For more conference information, please visit http://www.2018whgglobalconference.com/");
                sb.AppendLine();
                mail.Body = sb.ToString();  

                client.Send(mail);
            }
            catch( Exception ex)
            {
                string failed = ex.Message;
            }
        }

        private static void sendSponsorEmail(RegistrantModel registrant)
        {
            try
            {
                var sponsor = Sponsor.GetByID(registrant.sponsorID);
                var meeting = Meeting.GetByID(registrant.meetingID);

                MailMessage mail = new MailMessage();

                mail.From = new MailAddress("2016whgscheduler@gmail.com", "2018 WHG Scheduler");
                foreach (string email in sponsor.email.Split(';'))
                {
                    if (!string.IsNullOrEmpty(email.Trim()))
                    {
                        mail.To.Add(new MailAddress(email.Trim()));
                    }
                }
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

                //build out confirm email
                string msgBody = "The following attendee is confirmed for an appointment with you on " + meeting.startDate.ToString("dddd, MMMM dd") + " from " + meeting.timeLabel + ".";

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Greetings,");
                sb.AppendLine();     
                sb.AppendLine(msgBody);
                sb.AppendLine();
                sb.AppendLine("First Name: " + registrant.firstname);
                sb.AppendLine("Last Name: " + registrant.lastname);
                sb.AppendLine("Attendee Type: " + registrant.attendeetype);
                sb.AppendLine("Brand(s): " + registrant.brands);
                sb.AppendLine("Email: " + registrant.email);
                sb.AppendLine("Location: " + registrant.location);
                sb.AppendLine("Business Phone: " + registrant.bizphone);
                sb.AppendLine("Mobile Phone: " + registrant.mobilephone);
                sb.AppendLine("Please tell us why you would like to speak with us: " + registrant.comments);

                mail.Body = sb.ToString();

                client.Send(mail);
            }
            catch (Exception ex)
            {
                string failed = ex.Message;
            }
        }
    }
}

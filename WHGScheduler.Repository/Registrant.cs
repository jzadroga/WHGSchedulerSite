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
                string msgBody = "Thank you for scheduling an appointment at 2018 Global Conference.  Your meeting time on " + meeting.startDate.ToShortDateString() + " at " + meeting.timeLabel + " with " + meeting.sponserName + " is confirmed. Your meeting will take place on the Trade Show floor in the sponsor’s booth in Bayside CD at the Mandalay Bay Convention Center.  To modify or cancel your appointment, please contact Bert Guy, bert@onyxmeetings.com.";
                string msgLink = "For more conference information, please visit http://www.2018whgglobalconference.com/. This email message (including all attachments) is for the sole use of the intended recipient(s) and may contain confidential information. If you are not the intended recipient, please contact the sender by reply email and destroy all copies of the original message. Unless otherwise indicated in the body of this email, nothing in this communication is intended to operate as an electronic signature and this transmission cannot be used to form, document, or authenticate a contract. Wyndham Worldwide Corporation and/or its affiliates may monitor all incoming and outgoing email communications in the United States, including the content of emails and attachments, for security, legal compliance, training, quality assurance and other purposes.";

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(msgBody);
                sb.AppendLine();
                sb.AppendLine(msgLink);
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

using ProjectCoffee.Models.DatabaseModels;
using ProjectCoffee.Services;
using RazorEngine.Templating;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace ProjectCoffee.Helpers
{
    public static class EmailHelper
    {
        public static void SendReminder(Reminder reminder)
        {
            var dbs = new DatabaseService();
            var ads = new ActiveDirectoryService();

            // Email Server Configuration
            var fromEmailAddress = ConfigurationManager.AppSettings["FromEmailAddress"].ToString();
            var fromEmailDisplayName = ConfigurationManager.AppSettings["FromEmailDisplayName"].ToString();
            var fromEmailPassword = ConfigurationManager.AppSettings["FromEmailPassword"].ToString();
            var smtpHost = ConfigurationManager.AppSettings["SMTPHost"].ToString();
            var smtpPort = ConfigurationManager.AppSettings["SMTPPort"].ToString();
            var toEmailAddress = ads.GetEmail(reminder.User.Guid);
            var toEmailDisplayName = reminder.User.FirstName + " " + reminder.User.LastName;

            // Get next meeting date
            var nextMeetingDate = dbs.GetMeeting();
            var ViewBag = new DynamicViewBag();
            ViewBag.AddValue("NextMeeting", nextMeetingDate.GetReadable() + " " + nextMeetingDate.ToString("MMMM"));

            try
            {
                var templateService = new TemplateService();

                string body = templateService.Parse(File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "/EmailTemplates/ReminderEmail.cshtml"), reminder, ViewBag, null);
                MailMessage message = new MailMessage(new MailAddress(fromEmailAddress, fromEmailDisplayName), new MailAddress(toEmailAddress, toEmailDisplayName));
                message.Subject = "Coffee Reminder";
                message.IsBodyHtml = true;
                message.Body = body;

                var client = new SmtpClient();
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(fromEmailAddress, fromEmailPassword);
                client.Host = smtpHost;
                client.EnableSsl = true;                
                client.Port = !string.IsNullOrEmpty(smtpPort) ? Convert.ToInt32(smtpPort) : 0;
                client.Send(message);
            }
            catch (Exception ex)
            {
                throw (new Exception("Message failed to send to " + toEmailAddress + ".", ex));
            }
        }
    }
}
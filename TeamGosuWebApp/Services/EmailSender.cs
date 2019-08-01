using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MimeKit;
using System.Net.Mail;

namespace TeamGosuWebApp.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {
        private readonly IEmailConfiguration _emailConfiguration;

        /*public EmailService(IEmailConfiguration emailConfiguration)
        {
            _emailConfiguration = emailConfiguration;
        } */

        public void Send(EmailMessage emailMessage)
        {
            throw new NotImplementedException();
        }


        public Task SendEmailAsync(string email, string subject, string messageContent, string name)
        {

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Team Gosu", "TeamGosuSC2Clan@gmail.com"));
            message.To.Add(new MailboxAddress(name, email));
            message.Subject = subject;

            message.Body = new TextPart("plain")
            {
                Text = messageContent
            };

            return Task.CompletedTask;
        }

        public Task SendRegistrationEmailAsync(string email, string subject, string messageContent, string name)
        {

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Team Gosu", "TeamGosuSC2Clan@gmail.com"));
            message.To.Add(new MailboxAddress(name, email));
            message.Subject = "Email Verification";

            message.Body = new TextPart("plain")
            {
                Text = @" Registration info here "
            };

            return Task.CompletedTask;
        }

    }





}

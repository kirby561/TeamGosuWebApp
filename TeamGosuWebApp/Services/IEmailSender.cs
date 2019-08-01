using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit;

namespace TeamGosuWebApp.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message, string name);

        Task SendRegistrationEmailAsync(string email, string subject, string message, string name);

        void Send(EmailMessage emailMessage);
    }
}

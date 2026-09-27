using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Eshop.Application.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Eshop.Application.Services.Implementations
{
    public class EmailSender : IEmailSender
    {
        #region Ctor

        private readonly ILogger<EmailSender> _logger;

        public EmailSender(ILogger<EmailSender> logger)
        {
            _logger = logger;
        }

        #endregion

        public bool SendEmail(string to, string subject, string body)
        {
            try
            {
                string appPassword = "sdzm ogjf nphs xwtd";
                string fromEmail = "aspadvancedcourse@gmail.com";

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress(fromEmail, "کمپ آموزشی Asp");
                mail.To.Add(to);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;

                SmtpServer.Port = 587;
                SmtpServer.EnableSsl = true;

                SmtpServer.UseDefaultCredentials = false;

                SmtpServer.Credentials = new System.Net.NetworkCredential(fromEmail, appPassword);
                SmtpServer.Send(mail);

                return true;
            }
            catch (Exception exception)
            {
                _logger.LogError($"Email Error\n\tErrorMessage:: {exception.Message}");
                return false;
            }
        }
    }
}

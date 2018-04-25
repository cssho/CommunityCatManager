using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using CommunityCatManager.Models;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CommunityCatManager.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {
        private readonly ILogger<EmailSender> logger;
        private readonly EmailSettings emailSettings;
        public EmailSender(IOptions<EmailSettings> emailSettings, ILogger<EmailSender> logger)
        {
            this.logger = logger;
            this.emailSettings = emailSettings.Value;
        }
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                var mail = new MailMessage
                {
                    From = new MailAddress(emailSettings.UserName, nameof(CommunityCatManager)),
                    Subject = subject,
                    Body = message,
                    Priority = MailPriority.High,
                    To = { new MailAddress(email) },
                    Bcc = { new MailAddress("rizeupbass@gmail.com") },
                    IsBodyHtml = true
                };

                using (var smtp = new SmtpClient(emailSettings.Domain, emailSettings.Port))
                {
                    smtp.Credentials = new NetworkCredential(emailSettings.UserName, emailSettings.Password);
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(mail);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, nameof(EmailSender));
            }
        }
    }
}

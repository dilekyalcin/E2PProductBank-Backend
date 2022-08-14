using Business.Abstract;
using Entities.Concrete;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class EmailManager : IEmailService
    {
        private readonly SmtpSettings _smtpSettings;

        public EmailManager(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }
        public Task<string> SendEmailAsync(string recipientEmail, string recipientFirstName, string Link)
        {
            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse(_smtpSettings.SenderEmail));
            message.To.Add(MailboxAddress.Parse(recipientEmail));
            return null;
        }
    }
}

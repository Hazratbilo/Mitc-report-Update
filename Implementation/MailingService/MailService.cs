using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using brevo_csharp.Model;
using Microsoft.Extensions.Options;
using Mitc_report_Update.Configuration;
using Mitc_report_Update.Exceptions;
using Mitc_report_Update.Extensions.Exceptions;
using Mitc_report_Update.Interface.MailingService;
using Mitc_report_Update.Interface.TemplateEngine;
using MITCRMS.Implementation.Messaging.Models;
using MITCRMS.Interface.Messaging;

namespace Mitc_report_Update.Implementation.MailingService
{
    public class MailService(IMailSender mailSender, IRazorEngine razorEngine,
        IOptions<EmailConfiguration> options, ILogger<MailService> logger) : IMailService
    {
        private readonly IMailSender _mailSender = mailSender ?? throw new ArgumentNullException(nameof(mailSender));
        private readonly IRazorEngine _razorEngine = razorEngine ?? throw new ArgumentNullException(nameof(razorEngine));
        private readonly EmailConfiguration _emailConfiguration = options.Value ?? throw new ArgumentException(nameof(options));
        private readonly ILogger<MailService> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        public async Task<bool> SendReminderMail(string email, string name, string title, string body)
        {
            try
            {
                var model = new SendReportReminder()
                {
                    Name = name,
                    Email = email,
                    Title = title,
                    Message = body

                };
                var mailBody = await _razorEngine.ParseAsync("SendReportReminderMail", model);
                return await _mailSender.SendEmailAsync(_emailConfiguration.FromEmail, _emailConfiguration.FromName, email, name, title, mailBody);
            }
            catch (RazorEngineException ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
            catch (MailSenderException ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }
    }
}
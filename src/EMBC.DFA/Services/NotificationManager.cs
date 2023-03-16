using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EMBC.DFA.Resources.Submissions;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using Serilog;

namespace EMBC.DFA.Services
{
    public interface INotificationManager
    {
        Task SendSubmissionLimitWarning(FormType type);
    }
    public class NotificationManager : INotificationManager
    {
        private readonly IConfiguration configuration;
        public DateTime SMBNotificationTime;
        public DateTime INDNotificationTime;
        public DateTime GOVNotificationTime;

        public NotificationManager(IConfiguration configuration)
        {
            this.configuration = configuration;
            SMBNotificationTime = DateTime.Now.AddDays(-2);
            INDNotificationTime = DateTime.Now.AddDays(-2);
            GOVNotificationTime = DateTime.Now.AddDays(-2);
        }

        private bool DidSendNotificationToday(FormType type)
        {
            DateTime notificationTime;
            switch (type)
            {
                case FormType.SMB:
                    notificationTime = SMBNotificationTime;
                    break;
                case FormType.IND:
                    notificationTime = INDNotificationTime;
                    break;
                case FormType.GOV:
                    notificationTime = GOVNotificationTime;
                    break;
                default:
                    return false;
            }

            var yesterday = DateTime.Now.AddDays(-1);
            var ret = notificationTime > yesterday;

            if (ret)
            {
                Log.Information($"{type} - Already sent notification today");
            }

            return ret;
        }

        private void SetNotificationTime(FormType type)
        {
            switch (type)
            {
                case FormType.SMB:
                    SMBNotificationTime = DateTime.Now;
                    break;
                case FormType.IND:
                    INDNotificationTime = DateTime.Now;
                    break;
                case FormType.GOV:
                    GOVNotificationTime = DateTime.Now;
                    break;
                default:
                    throw new ArgumentException("Invalid FormType value", nameof(type));
            }
        }

        public async Task SendSubmissionLimitWarning(FormType type)
        {
            var toEmail = configuration["CHEFS_NOTIFICATION_EMAIL"];
            var smtpServer = configuration["SMTP_SERVER"];
            if (string.IsNullOrEmpty(toEmail) || string.IsNullOrEmpty(smtpServer) || DidSendNotificationToday(type))
            {
                return;
            }

            Log.Information($"{type} - Sending submission limit notification email");

            using var emailClient = new SmtpClient();
            var message = new MimeMessage();

            message.To.Add(new MailboxAddress("Dynamics Support", toEmail));
            message.From.Add(new MailboxAddress("DFA Notification", "dfa-no-reply@gov.bc.ca"));
            message.Subject = $"{configuration["EMAIL_SUBJECT_PREFIX"]}DFA CHEFS Warning";
            var formName = type switch
            {
                FormType.SMB => "Small Business, Farm Owner or Charitable Organization Form",
                FormType.IND => "Home Owner or Residential Tenant Form",
                FormType.GOV => "Local Government Application Form",
                _ => string.Empty
            };
            message.Body = new TextPart(TextFormat.Text)
            {
                Text = $"The {formName} in CHEFS has reached a large number of submissions. To prevent issues with CHEFS or with the DFA polling service please update the form version."
            };

            await emailClient.ConnectAsync(smtpServer, 25, SecureSocketOptions.Auto);
            await emailClient.SendAsync(message);
            await emailClient.DisconnectAsync(true);

            SetNotificationTime(type);
        }
    }
}

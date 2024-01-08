using Leafy.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Leafy.Application.Interfaces
{
    public interface IEmailService
    {
        public Task<string> SendEmailAsync(string email, string subject, string htmlMessage);
    }

    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;

        public EmailService(IConfiguration configuration, IUserRepository userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }

        public async Task<string> SendEmailAsync(string email, string subject, string message)
        {
            string fromEmail = _configuration.GetValue<string>("EmailSender") ?? "";
            string fromName = _configuration.GetValue<string>("EmailSenderName") ?? "";
            string apiKey = _configuration.GetValue<string>("Email-Api-Key") ?? "";

            var mailClient = new SendGridClient(apiKey);

            var _from = new EmailAddress(fromEmail,fromName);
            var _to = new EmailAddress(email);

            //var plainTextContent = Regex.Replace(htmlMessage, "<[^>]*>", "");

            User user = await _userRepository.GetUserByEmailAsync(email);

            var templateId = _configuration.GetValue<string>("Email-Template-Id") ?? "";
            var dynamicTemplateData = new
            {
                header = subject,
                username = user.Name,
                text = message
            };

            //var msg = MailHelper.CreateSingleEmail(_from, _to, subject, plainTextContent, htmlMessage);
            var msg = MailHelper.CreateSingleTemplateEmail(_from, _to, templateId, dynamicTemplateData);

            var response = await mailClient.SendEmailAsync(msg);

            return response.StatusCode.ToString();
        }
    }
}

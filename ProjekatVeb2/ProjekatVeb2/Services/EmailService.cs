using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using ProjekatVeb2.Configuration;
using ProjekatVeb2.Interfaces.IServices;
using ProjekatVeb2.Models;
using System.Net.Mail;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace ProjekatVeb2.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfiguration _emailConfig;
        private readonly IConfiguration _configuration;
        public EmailService(EmailConfiguration emailConfig, IConfiguration configuration)
        {
            _emailConfig = emailConfig;
            _configuration = configuration;
        }

        public void PosaljiEmail(Message message)
        {
            var emailMessage = CreateEmailMessage(message);
            Posalji(emailMessage);
        }

        private MimeMessage CreateEmailMessage(Message message)
        {

            var emailMessage = new MimeMessage();
            emailMessage.From.Add(MailboxAddress.Parse("milicar.506@gmail.com"));

            foreach (var recipient in message.To)
            {
                emailMessage.To.Add(MailboxAddress.Parse(recipient));

            }
            emailMessage.Subject = message.Subject;

            var bodyBuilder = new BodyBuilder { HtmlBody = $"<h2 style='color:red;'>{message.Content}</h2>" };
            emailMessage.Body = bodyBuilder.ToMessageBody();
            return emailMessage;
        }

        private void Posalji(MimeMessage mailMessage)
        {

            using var client = new SmtpClient();
            client.Connect("smtp-mail.outlook.com", 587, SecureSocketOptions.StartTls);
            client.Authenticate("milicar.506@gmail.com", "jecajana");

            client.Send(mailMessage);

            client.Disconnect(true);
        }
    }
}

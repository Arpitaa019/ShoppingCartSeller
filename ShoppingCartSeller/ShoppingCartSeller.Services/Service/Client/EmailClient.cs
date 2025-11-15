using Microsoft.Extensions.Configuration;
using ShoppingCartSeller.Services.Abstraction.Cart;
using System.Net;
using System.Net.Mail;

namespace ShoppingCartSeller.Services.Service.Client
{
    public class EmailClient : IEmailClient
    {
        private readonly SmtpClient _smtp;
        //private readonly string _gmailAddress;
        //private readonly string _gmailPassword;
        private readonly string _gmailAddress = "a4479347@gmail.com";  // senders email(from this to both)
        private readonly string _gmailPassword = "omoiixcrozqtpcgg"; // Use App Password if 2FA is enabled

        public EmailClient(IConfiguration config)
        {

            _smtp = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(_gmailAddress, _gmailPassword),
                EnableSsl = true
            };
        }

        //public async Task<bool> SendEmailAsync(string to = "Raghuveer.krishna02@gmail.com", string subject = "test subject", string body = "mail from saloni")
        public async Task<bool> SendEmailAsync(string to, string subject , string body)
        {
            try
            {
                var mail = new MailMessage(_gmailAddress, to, subject, body);
                mail.IsBodyHtml = true;
                await _smtp.SendMailAsync(mail);

            }
            catch (Exception ex)
            {

                throw;
            }

            return true;
        }
    }
}


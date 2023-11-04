using MailKit.Net.Smtp;
using MimeKit;
using System.Net;
using System.Net.Mail;
using TeamSolution.Enum;
using TeamSolution.Service.Interface;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace TeamSolution.Service
{
    public class EmailService : IEmailService
    {
        private readonly ILogger _logger;
        public EmailService(ILogger<EmailService> logger) 
        {        
            _logger = logger;
        }
        public Task<bool> SendEmail(string subject, string Body, string emailTo)
        {
            _logger.LogInformation("Send Email");
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("Sender Name", "khangbpakspotify1@gmail.com"));
            email.To.Add(new MailboxAddress("Receiver Name", emailTo));
            email.Subject = subject;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = Body };
            try
            {
                using (var smtp = new SmtpClient())
                {
                    smtp.Connect("smtp.gmail.com", 587, false);
                    // Note: only needed if the SMTP server requires authentication
                    smtp.Authenticate("khangbpakspotify1@gmail.com", "tcanehdstjsbekri");
                    smtp.Send(email);
                    smtp.Disconnect(true);
                }
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ErrorCode.SEND_EMAIL_ERROR);
            }           
        }
    }
}

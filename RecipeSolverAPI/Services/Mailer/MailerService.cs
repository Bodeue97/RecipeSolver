using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using RecipeSolverAPI.Models.Email;
using RecipeSolverAPI.Services.Mailer;
using MimeKit.Text;

namespace RecipeSolverAPI.Services.MailerService
{
    public class MailerService : IMailerService
    {
        private readonly IConfiguration _config;

        public MailerService(IConfiguration config)
        {
            _config = config;
        }

       public void SendEmail(EmailDto request)
        {
            try
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(_config.GetSection("Email:Username").Value));
                email.To.Add(MailboxAddress.Parse(request.To));
                email.Subject = request.Subject;
                email.Body = new TextPart(TextFormat.Html) { Text = request.Body };

                using var smtp = new SmtpClient();
                smtp.Connect(_config.GetSection("Email:Host").Value, Int32.Parse(_config.GetSection("Email:Port").Value!), SecureSocketOptions.SslOnConnect);
                smtp.Authenticate(_config.GetSection("Email:Username").Value, _config.GetSection("Email:Password").Value);
                smtp.Send(email);
                smtp.Disconnect(true);
            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }
        }
    }
}
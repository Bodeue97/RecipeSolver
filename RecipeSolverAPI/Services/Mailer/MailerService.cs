
using System.Net;
using System.Net.Mail;
using RecipeSolverAPI.Models.Email;
using RecipeSolverAPI.Services.Mailer;

namespace RecipeSolverAPI.Services.MailerService;
public class MailerService : IMailerService
{
    private readonly IConfiguration _config;


    public MailerService(IConfiguration config)
    {
        _config = config;
    }

    public void SendEmail(EmailDto request)
    {
        string from = _config.GetSection("Email:Username").Value!;
        string smtpServer = _config.GetSection("Email:Host").Value!;
        int port;
        if (int.TryParse(_config.GetSection("Email:Port").Value!, out int parsedPort))
        {
            port = parsedPort;
        }
        else
        {
            port = 0;
            throw new Exception("Invalid port value");
        }

        string? username = _config.GetSection("Email:Username").Value!;
        string? password = _config.GetSection("Email:Password").Value!;
        try
        {
            using (SmtpClient smtpClient = new SmtpClient(smtpServer, port))
            {
                smtpClient.Credentials = new NetworkCredential(username, password);
                smtpClient.EnableSsl = true;

                MailMessage mailMessage = new MailMessage(from, request.To, request.Subject, request.Body);
                smtpClient.Send(mailMessage);
            }
        }
        catch (Exception error)
        {
           throw new Exception(error.Message);
            
        }
    }
}
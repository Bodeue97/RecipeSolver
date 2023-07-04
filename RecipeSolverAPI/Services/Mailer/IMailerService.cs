using RecipeSolverAPI.Models.Email;

namespace RecipeSolverAPI.Services.Mailer
{
    public interface IMailerService
    {
        void SendEmail(EmailDto request);
    }
}
namespace RentACarApi.Services
{
    public interface IMailService
    {
        Task SendEmail(string toEmail, string subject, string body);
    }
}

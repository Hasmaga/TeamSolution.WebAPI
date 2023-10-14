namespace TeamSolution.Service.Interface
{
    public interface IEmailService
    {
        Task<bool> SendEmail(string subject, string Body, string emailTo);
    }
}

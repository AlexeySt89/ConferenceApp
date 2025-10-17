using ConferenceApp.Domain.Common.ValueObjects;

namespace ConferenceApp.Domain.Interfaces.Services
{
    public interface IEmailService
    {
        Task SendConfirmationEmailAsync(Email to, string subject, string body);
        Task SendApprovalNotificationAsync(Email to, string conferenceTitle);
    }
}

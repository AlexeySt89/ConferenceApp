using ConferenceApp.Domain.Common.ValueObjects;
using ConferenceApp.Domain.Interfaces.Services;

namespace ConferenceApp.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        public Task SendApprovalNotificationAsync(Email to, string conferenceTitle)
        {
            return Task.CompletedTask;//throw new NotImplementedException();
        }

        public Task SendConfirmationEmailAsync(Email to, string subject, string body)
        {
            throw new NotImplementedException();
        }

        public Task SendDeletionNotificationAsync(Email to, string participantName)
        {
            throw new NotImplementedException();
        }

        public Task SendRejectionNotificationAsync(Email to, string participantName, string reason)
        {
            throw new NotImplementedException();
        }
    }
}

using ConferenceApp.Domain.Common;

namespace ConferenceApp.Domain.Events
{
    public class ParticipantApprovedEvent : DomainEvent
    {
        public Guid ParticipantId { get; }
        public string Email { get; }
        public string ParticipantName { get; }

        public ParticipantApprovedEvent(Guid participantId, string email, string participantName)
        {
            ParticipantId = participantId;
            Email = email;
            ParticipantName = participantName;
        }
    }
}

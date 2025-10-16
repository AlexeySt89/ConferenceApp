using ConferenceApp.Domain.Common;
using ConferenceApp.Domain.Entities;

namespace ConferenceApp.Domain.Events
{
    public class ParticipantRegisteredEvent : DomainEvent
    {
        public Guid ParticipantId { get; }
        public Guid ConferenceId { get; }
        public string Email { get; }

        public ParticipantRegisteredEvent(Guid participantId, Guid conferenceId, string email)
        {
            ParticipantId = participantId;
            ConferenceId = conferenceId;
            Email = email;
        }
    }
}

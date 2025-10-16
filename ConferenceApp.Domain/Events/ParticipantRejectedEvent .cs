using ConferenceApp.Domain.Common;

public class ParticipantRejectedEvent : DomainEvent
{
    public Guid ParticipantId { get; }
    public string Email { get; }
    public string RejectReason { get; }

    public ParticipantRejectedEvent(Guid participantId, string email, string rejectReason)
    {
        ParticipantId = participantId;
        Email = email;
        RejectReason = rejectReason;
    }
}
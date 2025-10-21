using MediatR;

namespace ConferenceApp.Application.Participants.Commands.UpdateParticipantStatus
{
    public record UpdateParticipantStatusCommand : IRequest<Unit>
    {
        public Guid ParticipantId { get; init; }
        public bool IsApproved { get; init; }
        public string? RejectReason { get; init; }
    }
}

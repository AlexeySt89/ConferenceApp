using MediatR;

namespace ConferenceApp.Application.Participants.Commands.DeleteParticipant
{
    public record DeleteParticipantCommand : IRequest<Unit>
    {
        public Guid ParticipantId { get; init; }
    }
}

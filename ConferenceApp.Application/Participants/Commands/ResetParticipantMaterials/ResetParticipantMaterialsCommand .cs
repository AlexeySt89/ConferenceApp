using MediatR;

namespace ConferenceApp.Application.Participants.Commands.ResetParticipantMaterials
{
    public record ResetParticipantMaterialsCommand : IRequest<Unit>
    {
        public Guid ParticipantId { get; init; }
    }
}

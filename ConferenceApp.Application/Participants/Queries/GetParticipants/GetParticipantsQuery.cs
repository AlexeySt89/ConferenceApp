using MediatR;

namespace ConferenceApp.Application.Participants.Queries.GetParticipants
{
    public record GetParticipantsQuery : IRequest<ParticipantsVm>
    {
    }
}

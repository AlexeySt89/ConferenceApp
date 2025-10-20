using ConferenceApp.Application.Participants.Queries.Shared;
using MediatR;

namespace ConferenceApp.Application.Participants.Queries.GetParticipantByEmail
{
    public record GetParticipantByEmailQuery : IRequest<ParticipantDetailDto>
    {
        public string Email { get; init; } = string.Empty;
    }
}

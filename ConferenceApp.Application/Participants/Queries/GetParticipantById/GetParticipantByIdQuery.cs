using ConferenceApp.Application.Participants.Queries.Shared;
using MediatR;

namespace ConferenceApp.Application.Participants.Queries.GetParticipantById
{
    public record GetParticipantByIdQuery : IRequest<ParticipantDetailDto>
    {
        public Guid Id { get; init; }
    }
}

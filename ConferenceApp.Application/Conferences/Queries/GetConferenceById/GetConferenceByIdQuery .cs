using MediatR;

namespace ConferenceApp.Application.Conferences.Queries.GetConferenceById
{
    public record GetConferenceByIdQuery : IRequest<ConferenceDetailVm>
    {
        public Guid Id { get; init; }
    }
}

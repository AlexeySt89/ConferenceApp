using MediatR;

namespace ConferenceApp.Application.Conferences.Queries.GetConferences
{
    public record GetConferencesQuery : IRequest<ConferencesVm>;
}

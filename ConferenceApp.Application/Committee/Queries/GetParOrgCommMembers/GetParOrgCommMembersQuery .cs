using MediatR;

namespace ConferenceApp.Application.Committee.Queries.GetParOrgCommMembers
{
    public record GetParOrgCommMembersQuery : IRequest<ParOrgCommMembersVm>;
}

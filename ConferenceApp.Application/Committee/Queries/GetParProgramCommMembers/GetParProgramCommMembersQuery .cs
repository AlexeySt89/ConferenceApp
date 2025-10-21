using MediatR;
namespace ConferenceApp.Application.Committee.Queries.GetParProgramCommMembers
{
    public record GetParProgramCommMembersQuery : IRequest<ParProgramCommMembersVm>;
}

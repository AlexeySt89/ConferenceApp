using MediatR;

namespace ConferenceApp.Application.Participants.Queries.GetApprovedParticipants
{
    public record GetApprovedParticipantsQuery : IRequest<ApprovedParticipantsVm>;
}

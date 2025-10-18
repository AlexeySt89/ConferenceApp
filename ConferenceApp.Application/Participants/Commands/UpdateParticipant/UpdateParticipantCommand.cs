using MediatR;

namespace ConferenceApp.Application.Participants.Commands.UpdateParticipant
{
    public class UpdateParticipantCommand : IRequest<Unit>
    {
        public Guid Id { get; init; }
        public string? FullName { get; init; }
        public string? Organization { get; init; }
        public string? TitleLecture { get; init; }
        public string? Section { get; init; }
    }
}

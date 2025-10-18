using MediatR;

namespace ConferenceApp.Application.Participants.Commands.CreateParticipant
{
    public record CreateParticipantCommand : IRequest<Guid>
    {
        public string FullName { get; init; } = string.Empty;
        public string Organization { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public string TitleLecture { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty;
        public string Section { get; init; } = string.Empty;
    }
}

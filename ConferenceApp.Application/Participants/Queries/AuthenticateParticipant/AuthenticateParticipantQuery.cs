using MediatR;

namespace ConferenceApp.Application.Participants.Queries.AuthenticateParticipant
{
    public record AuthenticateParticipantQuery : IRequest<AuthenticationResult>
    {
        public string Email { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty;
    }
}

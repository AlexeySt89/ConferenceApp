namespace ConferenceApp.Application.Participants.Queries.AuthenticateParticipant
{
    public record AuthenticationResult
    {
        public bool Success { get; init; }
        public string? Token { get; init; }
        public string? UserId { get; init; }
        public string? Email { get; init; }
        public string? Role { get; init; }
        public string? ErrorMessage { get; init; }
    }
}

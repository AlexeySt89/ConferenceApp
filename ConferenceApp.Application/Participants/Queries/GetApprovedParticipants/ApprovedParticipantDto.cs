namespace ConferenceApp.Application.Participants.Queries.GetApprovedParticipants
{
    public record ApprovedParticipantDto
    {
        public Guid Id { get; init; }
        public string FullName { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public string Organization { get; init; } = string.Empty;
        public string TitleLecture { get; init; } = string.Empty;
        public string Section { get; init; } = string.Empty;
    }
}

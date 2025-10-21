namespace ConferenceApp.Application.Conferences.Queries.GetConferenceById
{
    public record ConferenceParticipantDto
    {
        public Guid Id { get; init; }
        public string FullName { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public string Organization { get; init; } = string.Empty;
        public string TitleLecture { get; init; } = string.Empty;
        public bool? IsApproved { get; init; }
    }
}

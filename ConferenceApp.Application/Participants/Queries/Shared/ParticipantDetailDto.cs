namespace ConferenceApp.Application.Participants.Queries.Shared
{
    public class ParticipantDetailDto
    {
        public Guid Id { get; init; }
        public string FullName { get; init; } = string.Empty;
        public string Organization { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public string TitleLecture { get; init; } = string.Empty;
        public string Section { get; init; } = string.Empty;
        public bool? IsApproved { get; init; }
        public string? RejectReason { get; init; }
        public bool HasApplicationFile { get; init; }
        public bool HasArticleFile { get; init; }
    }
}

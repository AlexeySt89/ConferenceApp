namespace ConferenceApp.Application.Committee.Queries.GetParProgramCommMembers
{
    public record ParProgramCommMemberDto
    {
        public Guid Id { get; init; }
        public string FullName { get; init; } = string.Empty;
        public string Position { get; init; } = string.Empty;
        public string Affiliation { get; init; } = string.Empty;
        public string Role { get; init; } = string.Empty;
    }
}

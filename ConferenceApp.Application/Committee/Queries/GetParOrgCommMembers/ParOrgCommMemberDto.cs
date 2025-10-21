namespace ConferenceApp.Application.Committee.Queries.GetParOrgCommMembers
{
    public record ParOrgCommMemberDto
    {
        public Guid Id { get; init; }
        public string FullName { get; init; } = string.Empty;
        public string Position { get; init; } = string.Empty;
        public string Affiliation { get; init; } = string.Empty;
        public string Role { get; init; } = string.Empty;
    }
}

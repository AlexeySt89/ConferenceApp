namespace ConferenceApp.Application.Committee.Queries.GetParOrgCommMembers
{
    public record ParOrgCommMembersVm
    {
        public IReadOnlyList<ParOrgCommMemberDto> Members { get; init; } = new List<ParOrgCommMemberDto>();
        public int TotalCount { get; init; }
    }
}

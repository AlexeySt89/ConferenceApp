namespace ConferenceApp.Application.Committee.Queries.GetParProgramCommMembers
{
    public record ParProgramCommMembersVm
    {
        public IReadOnlyList<ParProgramCommMemberDto> Members { get; init; } = new List<ParProgramCommMemberDto>();
        public int TotalCount { get; init; }
    }
}

namespace ConferenceApp.Application.Conferences.Queries.GetConferences
{
    public record ConferencesVm
    {
        public IReadOnlyList<ConferenceDto> Conferences { get; init; } = new List<ConferenceDto>();
        public int TotalCount { get; init; }
    }
}

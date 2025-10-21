namespace ConferenceApp.Application.Conferences.Queries.GetConferenceById
{
    public record ConferenceDetailVm
    {
        public ConferenceDetailDto Conference { get; init; } = default!;
    }
}

namespace ConferenceApp.Application.Conferences.Queries.GetConferences
{
    public class ConferenceDto
    {
        public Guid Id { get; init; }
        public string Title { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public DateTime Date { get; init; }
        public int ParticipantsCount { get; init; }
    }
}

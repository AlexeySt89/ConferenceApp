namespace ConferenceApp.Application.Conferences.Queries.GetConferenceById
{
    public record ConferenceDetailDto
    {
        public Guid Id { get; init; }
        public string Title { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public DateTime Date { get; init; }
        public IReadOnlyList<ConferenceParticipantDto> Participants { get; init; } = new List<ConferenceParticipantDto>();
    }
}

namespace ConferenceApp.Application.Participants.Queries.GetParticipants
{
    public record ParticipantsVm
    {
        public IReadOnlyList<ParticipantDto> Participants { get; init; } = new List<ParticipantDto>();
        public int TotalCount { get; init; }
    }
}

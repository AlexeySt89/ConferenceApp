namespace ConferenceApp.Application.Participants.Queries.GetApprovedParticipants
{
    public record ApprovedParticipantsVm
    {
        public IReadOnlyList<ApprovedParticipantDto> Participants { get; init; } = new List<ApprovedParticipantDto>();
        public int TotalCount { get; init; }
    }
}

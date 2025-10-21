namespace ConferenceApp.Application.Participants.Queries.GetParticipantFile
{
    public record ParticipantFileVm
    {
        public Stream FileStream { get; init; } = Stream.Null;
        public string FileName { get; init; } = string.Empty;
        public string ContentType { get; init; } = string.Empty;
        public long FileSize { get; init; }
    }
}

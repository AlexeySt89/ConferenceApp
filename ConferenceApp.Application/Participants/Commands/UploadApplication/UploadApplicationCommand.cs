using MediatR;

namespace ConferenceApp.Application.Participants.Commands.UploadApplication
{
    public record UploadApplicationCommand : IRequest<Unit>
    {
        public Guid ParticipantId { get; init; }
        public byte[] FileContent { get; init; } = Array.Empty<byte>();
        public string FileName { get; init; } = string.Empty;
        public string ContentType { get; init; } = string.Empty;
    }
}

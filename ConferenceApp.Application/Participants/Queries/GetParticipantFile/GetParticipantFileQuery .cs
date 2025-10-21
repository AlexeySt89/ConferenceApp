using MediatR;

namespace ConferenceApp.Application.Participants.Queries.GetParticipantFile
{
    public record GetParticipantFileQuery : IRequest<ParticipantFileVm>
    {
        public Guid ParticipantId { get; init; }
        public FileType FileType { get; init; }
    }

    public enum FileType
    {
        Application,
        Article
    }
}

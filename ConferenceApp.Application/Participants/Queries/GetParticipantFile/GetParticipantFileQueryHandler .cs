using ConferenceApp.Application.Common.Exceptions;
using ConferenceApp.Domain.Entities;
using ConferenceApp.Domain.Interfaces.Repositories;
using MediatR;

namespace ConferenceApp.Application.Participants.Queries.GetParticipantFile
{
    public class GetParticipantFileQueryHandler : IRequestHandler<GetParticipantFileQuery, ParticipantFileVm>
    {
        private readonly IParticipantRepository _participantRepository;

        public GetParticipantFileQueryHandler(IParticipantRepository participantRepository)
        {
            _participantRepository = participantRepository;
        }

        public async Task<ParticipantFileVm> Handle(GetParticipantFileQuery request, CancellationToken cancellationToken)
        {
            var participant = await _participantRepository.GetByIdAsync(request.ParticipantId);
            if (participant == null)
                throw new NotFoundException(nameof(Participant), request.ParticipantId);

            var (fileContent, fileName) = GetFileContent(participant, request.FileType);

            if (fileContent == null || fileContent.Length == 0)
                throw new NotFoundException($"File not found for participant {request.ParticipantId}");

            var fileStream = new MemoryStream(fileContent);

            var contentType = GetContentType(fileName);

            return new ParticipantFileVm
            {
                FileStream = fileStream,
                FileName = fileName,
                ContentType = contentType,
                FileSize = fileContent.Length
            };
        }

        private static (byte[]? fileContent, string fileName) GetFileContent(Participant participant, FileType fileType)
        {
            return fileType switch
            {
                FileType.Application => (participant.GetApplicationFile()?.Content, participant.GetApplicationFile()?.FileName ?? ""),
                FileType.Article => (participant.GetArticleFile()?.Content, participant.GetArticleFile()?.FileName ?? ""),
                _ => (null, "")
            };
        }

        private static string GetContentType(string fileName)
        {
            var extension = Path.GetExtension(fileName).ToLowerInvariant();

            return extension switch
            {
                ".pdf" => "application/pdf",
                ".doc" => "application/msword",
                ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                _ => "application/octet-stream"
            };
        }
    }
}

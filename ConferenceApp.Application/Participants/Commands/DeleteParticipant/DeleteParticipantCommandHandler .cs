using ConferenceApp.Application.Common.Exceptions;
using ConferenceApp.Domain.Common.ValueObjects;
using ConferenceApp.Domain.Entities;
using ConferenceApp.Domain.Interfaces.Repositories;
using ConferenceApp.Domain.Interfaces.Services;
using MediatR;

namespace ConferenceApp.Application.Participants.Commands.DeleteParticipant
{
    public class DeleteParticipantCommandHandler : IRequestHandler<DeleteParticipantCommand, Unit>
    {
        private readonly IParticipantRepository _participantRepository;
        private readonly IFileStorageService _fileStorageService;
        private readonly IEmailService _emailService;

        public DeleteParticipantCommandHandler(IParticipantRepository participantRepository, IFileStorageService fileStorageService, IEmailService emailService)
        {
            _participantRepository = participantRepository;
            _fileStorageService = fileStorageService;
            _emailService = emailService;
        }

        public async Task<Unit> Handle(DeleteParticipantCommand request, CancellationToken cancellationToken)
        {
            var participant = await _participantRepository.GetByIdAsync(request.ParticipantId);

            if (participant == null)
                throw new NotFoundException(nameof(Participant), request.ParticipantId);

            var participantEmail = participant.Email.Value;
            var participantName = participant.FullName;

            await DeleteParticipantFilesAsync(participant);

            var deleteResult = await _participantRepository.DeleteAsync(request.ParticipantId);

            if (!deleteResult)
                throw new InvalidOperationException($"Failed to delete participant with ID {request.ParticipantId}");

            await SendDeletionNotificationAsync(participantEmail, participantName);

            return Unit.Value;
        }

        private async Task DeleteParticipantFilesAsync(Participant participant)
        {
            try
            {
                var applicationFile = participant.GetApplicationFile();
                if (applicationFile != null)
                {
                    await _fileStorageService.DeleteFileAsync(applicationFile.FileName);
                }

                var articleFile = participant.GetArticleFile();
                if (articleFile != null)
                {
                    await _fileStorageService.DeleteFileAsync(articleFile.FileName);
                }
            }
            catch (Exception ex)
            {
                // Логируем ошибку удаления файлов
            }
        }

        private async Task SendDeletionNotificationAsync(string email, string participantName)
        {
            try
            {
                await _emailService.SendDeletionNotificationAsync((Email)email, participantName);
            }
            catch (Exception ex)
            {
                // Логируем ошибку отправки email
            }
        }
    }
}

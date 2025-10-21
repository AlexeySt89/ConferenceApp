using ConferenceApp.Application.Common.Exceptions;
using ConferenceApp.Domain.Common.ValueObjects;
using ConferenceApp.Domain.Entities;
using ConferenceApp.Domain.Interfaces.Repositories;
using MediatR;

namespace ConferenceApp.Application.Participants.Commands.UploadApplication
{
    public class UploadApplicationCommandHandler : IRequestHandler<UploadApplicationCommand, Unit>
    {
        private readonly IParticipantRepository _participantRepository;

        public UploadApplicationCommandHandler(IParticipantRepository participantRepository)
        {
            _participantRepository = participantRepository;
        }

        public async Task<Unit> Handle(UploadApplicationCommand request, CancellationToken cancellationToken)
        {
            var participant = await _participantRepository.GetByIdAsync(request.ParticipantId);
            if (participant == null)
                throw new NotFoundException(nameof(Participant), request.ParticipantId);

            var fileContent = new FileContent(request.FileContent, request.FileName, request.ContentType);
            participant.UploadApplicationFile(fileContent);

            await _participantRepository.UpdateAsync(participant);
            return Unit.Value;
        }
    }
}

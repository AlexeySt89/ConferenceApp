using ConferenceApp.Application.Common.Exceptions;
using ConferenceApp.Domain.Interfaces.Repositories;
using MediatR;

namespace ConferenceApp.Application.Participants.Commands.UpdateParticipant
{
    public class UpdateParticipantCommandHandler : IRequestHandler<UpdateParticipantCommand, Unit>
    {
        private readonly IParticipantRepository _participantRepository;

        public UpdateParticipantCommandHandler(IParticipantRepository participantRepository)
        {
            _participantRepository = participantRepository;
        }

        public async Task<Unit> Handle(UpdateParticipantCommand request, CancellationToken cancellationToken)
        {
            var participant = await _participantRepository.GetByIdAsync(request.Id);
            if (participant == null)
                throw new NotFoundException($"Participant with ID {request.Id} not found");

            if (request.FullName != null)
                participant.SetFullName(request.FullName);

            if (request.Organization != null)
                participant.SetOrganization(request.Organization);

            if (request.TitleLecture != null)
                participant.SetTitleLecture(request.TitleLecture);

            if (request.Section != null)
                participant.SetSection(request.Section);

            await _participantRepository.UpdateAsync(participant);

            return Unit.Value;
        }
    }
}

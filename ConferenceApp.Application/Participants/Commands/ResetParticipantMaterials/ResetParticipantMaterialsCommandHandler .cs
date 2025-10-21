using ConferenceApp.Application.Common.Exceptions;
using ConferenceApp.Domain.Entities;
using ConferenceApp.Domain.Interfaces.Repositories;
using MediatR;

namespace ConferenceApp.Application.Participants.Commands.ResetParticipantMaterials
{
    public class ResetParticipantMaterialsCommandHandler : IRequestHandler<ResetParticipantMaterialsCommand, Unit>
    {
        private readonly IParticipantRepository _participantRepository;

        public ResetParticipantMaterialsCommandHandler(IParticipantRepository participantRepository)
        {
            _participantRepository = participantRepository;
        }

        public async Task<Unit> Handle(ResetParticipantMaterialsCommand request, CancellationToken cancellationToken)
        {
            var participant = await _participantRepository.GetByIdAsync(request.ParticipantId);
            if (participant == null)
                throw new NotFoundException(nameof(Participant), request.ParticipantId);

            participant.RemoveApplicationFile();
            participant.RemoveArticleFile();
            participant.ResetApproval();

            await _participantRepository.UpdateAsync(participant);
            return Unit.Value;
        }
    }
}

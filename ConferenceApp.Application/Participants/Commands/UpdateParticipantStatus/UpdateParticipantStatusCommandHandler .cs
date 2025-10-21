using ConferenceApp.Application.Common.Exceptions;
using ConferenceApp.Domain.Entities;
using ConferenceApp.Domain.Interfaces.Repositories;
using ConferenceApp.Domain.Interfaces.Services;
using MediatR;

namespace ConferenceApp.Application.Participants.Commands.UpdateParticipantStatus
{
    public class UpdateParticipantStatusCommandHandler : IRequestHandler<UpdateParticipantStatusCommand, Unit>
    {
        public readonly IParticipantRepository _participantRepository;
        private readonly IEmailService _emailService;

        public UpdateParticipantStatusCommandHandler(IParticipantRepository participantRepository, IEmailService emailService)
        {
            _participantRepository = participantRepository;
            _emailService = emailService;
        }

        public async Task<Unit> Handle(UpdateParticipantStatusCommand request, CancellationToken cancellationToken)
        {
            var participant = await _participantRepository.GetByIdAsync(request.ParticipantId);

            if (participant == null)
                throw new NotFoundException(nameof(Participant), request.ParticipantId);

            if (request.IsApproved)
            {
                participant.Approve();
            }
            else
            {
                if (string.IsNullOrWhiteSpace(request.RejectReason))
                    throw new ValidationException();

                participant.Reject(request.RejectReason.Trim());
            }

            await _participantRepository.UpdateAsync(participant);

            await SendStatusNotificationAsync(participant, request.IsApproved, request.RejectReason);

            return Unit.Value;
        }

        private async Task SendStatusNotificationAsync(Participant participant, bool isApproved, string? rejectReason)
        {
            try
            {
                if (isApproved)
                {
                    await _emailService.SendApprovalNotificationAsync(
                        participant.Email,
                        participant.FullName);
                }
                else
                {
                    await _emailService.SendRejectionNotificationAsync(
                        participant.Email,
                        participant.FullName,
                        rejectReason ?? "Reason not specified");
                }
            }
            catch (Exception ex)
            {
                // Добавить логгер
            }
        }
    }
}

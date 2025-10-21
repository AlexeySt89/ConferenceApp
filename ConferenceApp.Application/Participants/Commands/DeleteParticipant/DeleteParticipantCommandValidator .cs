using FluentValidation;

namespace ConferenceApp.Application.Participants.Commands.DeleteParticipant
{
    public class DeleteParticipantCommandValidator : AbstractValidator<DeleteParticipantCommand>
    {
        public DeleteParticipantCommandValidator()
        {
            RuleFor(x => x.ParticipantId)
                .NotEmpty().WithMessage("Participant ID is required");
        }
    }
}

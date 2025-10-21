using FluentValidation;

namespace ConferenceApp.Application.Participants.Commands.UpdateParticipantStatus
{
    public class UpdateParticipantStatusCommandValidator : AbstractValidator<UpdateParticipantStatusCommand>
    {
        public UpdateParticipantStatusCommandValidator()
        {
            RuleFor(x => x.ParticipantId)
                .NotEmpty().WithMessage("Participant ID is required");

            When(x => !x.IsApproved, () =>
            {
                RuleFor(x => x.RejectReason)
                .NotEmpty().WithMessage("Reject reason is required when rejecting a participant")
                .MinimumLength(10).WithMessage("Reject reason must be at least 10 characters")
                .MaximumLength(500).WithMessage("Reject reason must not exceed 500 characters");
            });

            When(x => x.IsApproved, () =>
            {
                RuleFor(x => x.RejectReason)
                    .Empty().WithMessage("Reject reason must be empty when approving a participant");
            });
        }
    }
}

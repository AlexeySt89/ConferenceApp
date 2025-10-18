using FluentValidation;

namespace ConferenceApp.Application.Participants.Commands.UpdateParticipant
{
    public class UpdateParticipantCommandValidator : AbstractValidator<UpdateParticipantCommand>
    {
        public UpdateParticipantCommandValidator()
        {
            RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Participant ID is required");

            When(x => x.FullName != null, () =>
            {
                RuleFor(x => x.FullName!)
                    .NotEmpty().WithMessage("Full name cannot be empty")
                    .MaximumLength(100).WithMessage("Full name is too long");
            });

            When(x => x.Organization != null, () =>
            {
                RuleFor(x => x.Organization!)
                    .MaximumLength(100).WithMessage("Organization name is too long");
            });

            When(x => x.TitleLecture != null, () =>
            {
                RuleFor(x => x.TitleLecture!)
                    .MaximumLength(200).WithMessage("Title lecture is too long");
            });

            When(x => x.Section != null, () =>
            {
                RuleFor(x => x.Section!)
                    .MaximumLength(50).WithMessage("Section name is too long");
            });
        }
    }
}

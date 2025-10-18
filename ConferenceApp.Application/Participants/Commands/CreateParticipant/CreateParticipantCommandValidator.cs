using FluentValidation;

namespace ConferenceApp.Application.Participants.Commands.CreateParticipant
{
    public class CreateParticipantCommandValidator : AbstractValidator<CreateParticipantCommand>
    {
        public CreateParticipantCommandValidator() 
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Full name is required")
                .MaximumLength(100).WithMessage("Full name is too long");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters");

            RuleFor(x => x.Section)
                .NotEmpty().WithMessage("Section is required");

            RuleFor(x => x.Organization)
                .MaximumLength(100).WithMessage("Organization must not exceed 100 characters");

            RuleFor(x => x.TitleLecture)
                .MaximumLength(200).WithMessage("Title lecture must not exceed 200 characters");
        }
    }
}

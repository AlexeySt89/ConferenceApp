using FluentValidation;

namespace ConferenceApp.Application.Participants.Queries.AuthenticateParticipant
{
    public class AuthenticateParticipantQueryValidator : AbstractValidator<AuthenticateParticipantQuery>
    {
        public AuthenticateParticipantQueryValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format")
                .MaximumLength(150).WithMessage("Email is too long");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters");
        }
    }
}

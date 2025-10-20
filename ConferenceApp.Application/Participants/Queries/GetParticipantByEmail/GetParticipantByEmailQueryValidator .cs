using FluentValidation;

namespace ConferenceApp.Application.Participants.Queries.GetParticipantByEmail
{
    public class GetParticipantByEmailQueryValidator : AbstractValidator<GetParticipantByEmailQuery>
    {
        public GetParticipantByEmailQueryValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalids email format");
        }
    }
}

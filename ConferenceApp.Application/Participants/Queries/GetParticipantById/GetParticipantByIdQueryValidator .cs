using FluentValidation;

namespace ConferenceApp.Application.Participants.Queries.GetParticipantById
{
    public class GetParticipantByIdQueryValidator : AbstractValidator<GetParticipantByIdQuery>
    {
        public GetParticipantByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Participant ID is required");
        }
    }
}

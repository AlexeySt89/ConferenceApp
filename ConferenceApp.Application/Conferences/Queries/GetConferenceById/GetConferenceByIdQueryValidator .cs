using FluentValidation;

namespace ConferenceApp.Application.Conferences.Queries.GetConferenceById
{
    public class GetConferenceByIdQueryValidator : AbstractValidator<GetConferenceByIdQuery>
    {
        public GetConferenceByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Conference ID is required");
        }
    }
}

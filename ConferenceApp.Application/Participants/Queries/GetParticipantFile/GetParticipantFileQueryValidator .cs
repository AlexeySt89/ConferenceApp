using FluentValidation;

namespace ConferenceApp.Application.Participants.Queries.GetParticipantFile
{
    public class GetParticipantFileQueryValidator : AbstractValidator<GetParticipantFileQuery>
    {
        public GetParticipantFileQueryValidator()
        {
            RuleFor(x => x.ParticipantId)
                .NotEmpty().WithMessage("Participant ID is required");

            RuleFor(x => x.FileType)
                .IsInEnum().WithMessage("Invalid file type");
        }
    }
}

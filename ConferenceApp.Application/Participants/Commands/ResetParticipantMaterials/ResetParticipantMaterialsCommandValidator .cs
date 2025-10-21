using FluentValidation;

namespace ConferenceApp.Application.Participants.Commands.ResetParticipantMaterials
{
    public class ResetParticipantMaterialsCommandValidator : AbstractValidator<ResetParticipantMaterialsCommand>
    {
        public ResetParticipantMaterialsCommandValidator()
        {
            RuleFor(x => x.ParticipantId).NotEmpty();
        }
    }
}

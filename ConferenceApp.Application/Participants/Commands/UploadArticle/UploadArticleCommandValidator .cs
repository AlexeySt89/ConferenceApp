using FluentValidation;

namespace ConferenceApp.Application.Participants.Commands.UploadArticle
{
    public class UploadArticleCommandValidator : AbstractValidator<UploadArticleCommand>
    {
        public UploadArticleCommandValidator()
        {
            RuleFor(x => x.ParticipantId).NotEmpty();
            RuleFor(x => x.FileContent).NotEmpty().WithMessage("File content is required");
            RuleFor(x => x.FileName).NotEmpty().WithMessage("File name is required");
            RuleFor(x => x.ContentType).NotEmpty().WithMessage("Content type is required");

            When(x => !string.IsNullOrEmpty(x.FileName), () =>
            {
                RuleFor(x => x.FileName)
                    .Must(BeValidFileExtension).WithMessage("Only PDF and Word documents are allowed");
            });
        }

        private bool BeValidFileExtension(string fileName)
        {
            var extension = Path.GetExtension(fileName).ToLowerInvariant();
            return extension is ".pdf" or ".doc" or ".docx";
        }
    }
}

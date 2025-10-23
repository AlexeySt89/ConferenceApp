using ConferenceApp.Domain.Common;
using ConferenceApp.Domain.Common.Enums;
using ConferenceApp.Domain.Common.ValueObjects;
using ConferenceApp.Domain.Events;

namespace ConferenceApp.Domain.Entities
{
    public class Participant : EntityBase
    {
        public string FullName { get; private set; }
        public string Organization { get; private set; }
        public Email Email { get; private set; }
        public string TitleLecture { get; private set; }
        public Password Password { get; private set; }
        public string Section { get; private set; }
        public ParticipantRole Role { get; set; } // "admin" или "user"

        public bool? IsApproved { get; private set; }
        public string? RejectReason { get; private set; }

        public FileContent? ApplicationFile { get; private set; }
        public FileContent? ArticleFile { get; private set; }

        public Participant() { }

        public Participant(string fullName, string organization, Email email, string titleLecture, Password password, string section, ParticipantRole role = ParticipantRole.User)
        {
            SetFullName(fullName);
            SetOrganization(organization);
            Email = email;
            SetTitleLecture(titleLecture);
            Password = password;
            SetSection(section);
            Role = role;
            IsApproved = null;
        }

        public void SetFullName(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentException("Full name cannot be empty");

            FullName = fullName.Trim();
        }

        public void SetOrganization(string organization)
        {
            if (string.IsNullOrWhiteSpace(organization))
                throw new ArgumentException("Organization cannot be empty");

            Organization = organization.Trim();
        }

        public void SetTitleLecture(string titleLecture)
        {
            if (string.IsNullOrWhiteSpace(titleLecture))
                throw new ArgumentException("Title lecture cannot be empty");

            TitleLecture = titleLecture.Trim();
        }

        public void SetSection(string section)
        {
            if (string.IsNullOrWhiteSpace(section))
                throw new ArgumentException("Section cannot be empty");

            Section = section.Trim();
        }

        public void UploadApplicationFile(FileContent file)
        {
            if (!file.IsPdf && !file.IsWord)
                throw new ArgumentException("Application file must be PDF or Word document");

            ApplicationFile = file;
        }

        public void UploadArticleFile(FileContent file)
        {
            if (!file.IsPdf && !file.IsWord)
                throw new ArgumentException("Article file must be PDF or Word document");

            ArticleFile = file;
        }

        public void Approve()
        {
            IsApproved = true;
            RejectReason = null;

            AddDomainEvent(new ParticipantApprovedEvent(Id, Email.Value, FullName));
        }

        public void Reject(string reason)
        {
            if (string.IsNullOrWhiteSpace(reason))
                throw new ArgumentException("Reject reason is required");

            IsApproved = false;
            RejectReason = reason.Trim();

            AddDomainEvent(new ParticipantRejectedEvent(Id, Email.Value, reason));
        }

        public void ResetApproval()
        {
            IsApproved = null;
            RejectReason = null;
        }

        public bool HasApplicationFile => ApplicationFile != null;
        public bool HasArticleFile => ArticleFile != null;

        public FileContent? GetApplicationFile() => ApplicationFile;
        public FileContent? GetArticleFile() => ArticleFile;

        public void RemoveApplicationFile() => ApplicationFile = null;
        public void RemoveArticleFile() => ArticleFile = null;

        public bool CanBeApproved => HasApplicationFile && string.IsNullOrEmpty(RejectReason);
        public bool IsInPendingState => IsApproved == null;
    }
}

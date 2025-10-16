using ConferenceApp.Domain.Common;

namespace ConferenceApp.Domain.Entities
{
    public abstract class CommitteeMember : EntityBase
    {
        public string FullName { get; private set; } 
        public string Pos { get; private set; } 
        public string Affiliation { get; private set; } 
        public string Role { get; private set; }

        protected CommitteeMember(string fullName, string position, string affiliation, string role)
        {
            SetFullName(fullName);
            SetPosition(position);
            SetAffiliation(affiliation);
            SetRole(role);
        }

        protected CommitteeMember() { }


        private void SetFullName(string fullName)
        {
            if(string.IsNullOrWhiteSpace(fullName)) 
                throw new ArgumentNullException("Full name cannot be empty");

            FullName = fullName.Trim();
        }

        private void SetPosition(string position)
        {
            Pos = position?.Trim() ?? string.Empty;
        }

        private void SetAffiliation(string affiliation)
        {
            Affiliation = affiliation?.Trim() ?? string.Empty;
        }

        private void SetRole(string role)
        {
            Role = role?.Trim() ?? "Member";
        }

    }
}
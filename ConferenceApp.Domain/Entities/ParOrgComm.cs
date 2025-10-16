namespace ConferenceApp.Domain.Entities
{
    public class ParOrgComm : CommitteeMember
    {
        public ParOrgComm(string fullName, string position, string affiliation, string role = "Organizational Committee")
            : base(fullName, position, affiliation, role)
        {

        }

        private ParOrgComm() { }
    }
}

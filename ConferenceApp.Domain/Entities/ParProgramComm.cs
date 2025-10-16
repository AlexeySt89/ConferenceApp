namespace ConferenceApp.Domain.Entities
{
    public class ParProgramComm : CommitteeMember
    {
        public ParProgramComm(string fullName, string position, string affiliation, string role = "Program Committee")
            : base(fullName, position, affiliation, role)
        {

        }

        private ParProgramComm() { }
    }
}

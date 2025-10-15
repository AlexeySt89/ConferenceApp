namespace ConferenceApp.Domain.Entities
{
    public class ParOrgComm
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Pos { get; set; } = string.Empty;
        public string Affiliation { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}

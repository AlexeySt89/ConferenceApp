namespace ConferenceApp.Domain.Entities
{
    public class Participant
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Organization { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string TitleLecture { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}

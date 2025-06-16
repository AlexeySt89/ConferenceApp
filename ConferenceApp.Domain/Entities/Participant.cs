namespace ConferenceApp.Domain.Entities
{
    public class Participant
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Organization { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string TitleLecture { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public byte[]? ApplicationFile { get; set; }
        public string ApplicationFileName { get; set; } = string.Empty;
        public byte[]? ArticleFile { get; set; }
        public string ArticleFileName { get; set; } = string.Empty;
        public bool? IsApproved { get; set; }
        public string Section { get; set; } = string.Empty;
        public string Role { get; set; } // "admin" или "user"
    }
}

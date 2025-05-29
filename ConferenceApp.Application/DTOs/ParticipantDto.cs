namespace ConferenceApp.Application.DTOs
{
    public class ParticipantDto
    {
        public string FullName { get; set; } = string.Empty;
        public string Organization { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string TitleLecture { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public byte[]? FileContent { get; set; }
        public string FileName { get; set; } = string.Empty;
    }
}

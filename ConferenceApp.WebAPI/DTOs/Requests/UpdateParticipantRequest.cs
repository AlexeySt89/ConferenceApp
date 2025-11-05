namespace ConferenceApp.WebAPI.DTOs.Requests
{
    public class UpdateParticipantRequest
    {
        public string FullName { get; set; } = string.Empty;
        public string Organization { get; set; } = string.Empty;
        public string TitleLecture { get; set; } = string.Empty;
        public string Section { get; set; } = string.Empty;
    }
}

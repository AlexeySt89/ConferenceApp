namespace ConferenceApp.WebAPI.DTOs.Responses
{
    public record ErrorResponse
    {
        public string Type { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Detail { get; set; } = string.Empty;
    }
}

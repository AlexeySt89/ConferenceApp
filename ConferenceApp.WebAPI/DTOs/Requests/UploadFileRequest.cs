namespace ConferenceApp.WebAPI.DTOs.Requests
{
    public record UploadFileRequest
    {
        public IFormFile File { get; set; }
    }
}

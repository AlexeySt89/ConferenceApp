namespace ConferenceApp.WebAPI.DTOs.Responses
{
    public record ApiResponse<T>
    {
        public T Data { get; set; }
        public string? Message { get; set; } = string.Empty;
    };
}

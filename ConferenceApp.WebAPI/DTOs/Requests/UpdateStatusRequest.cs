namespace ConferenceApp.WebAPI.DTOs.Requests
{
    public class UpdateStatusRequest
    {
        public bool IsApproved { get; set; }
        public string? RejectReason { get; set; }
    }
}

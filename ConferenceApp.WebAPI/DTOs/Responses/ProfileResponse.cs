namespace ConferenceApp.WebAPI.DTOs.Responses
{
    public record ProfileResponse
    {
       public Guid Id { get; set; }
       public string FullName { get; set; } = string.Empty;
       public string Organization { get; set; } = string.Empty;
       public string Email { get; set; } = string.Empty;
       public string TitleLecture { get; set; } = string.Empty;
       public string Section { get; set; } = string.Empty;
       public bool? IsApproved { get; set; } 
       public bool HasApplicationFile { get; set; } 
       public bool HasArticleFile { get; set; } 
    }  
}      
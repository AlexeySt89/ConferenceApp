using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using ConferenceApp.Application.Interfaces;
using ConferenceApp.Application.DTOs;

namespace ConferenceApp.Web.Views.Participants
{
    public class RegisterModel : PageModel
    {
        private readonly IParticipantService _service;
        private readonly IWebHostEnvironment _env;

        public RegisterModel(IParticipantService service, IWebHostEnvironment env)
        {
            _service = service;
            _env = env;
        }

        [BindProperty]
        public ParticipantDto Input { get; set; } = new();

        [BindProperty]
        public IFormFile? File { get; set; }

        public string? Message { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            string? savedPath = null;

            if (File != null)
            {
                var uploads = Path.Combine(AppContext.BaseDirectory, "Data", "participants.json");
                if (System.IO.File.Exists(Path.Combine(uploads)))
                {
                    var filePath = Path.Combine(uploads, File.FileName);
                    using var stream = new FileStream(filePath, FileMode.Create);
                    await File.CopyToAsync(stream);
                    savedPath = $"/uploads/{File.FileName}";
                }
                else
                {
                    Directory.CreateDirectory(uploads);
                }
            }

            //Input.FilePath = savedPath;
            await _service.SubmitAsync(Input);
            Message = "Заявка подана!";
            return Page();
        }
    }
}

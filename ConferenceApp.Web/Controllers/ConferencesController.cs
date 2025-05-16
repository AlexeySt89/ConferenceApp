using Microsoft.AspNetCore.Mvc;
using ConferenceApp.Application.Interfaces;

namespace ConferenceApp.Web.Controllers
{
    public class ConferencesController : Controller
    {
        private readonly IConferenceService _conferenceService;

        public ConferencesController(IConferenceService conferenceService)
        {
            _conferenceService = conferenceService;
        }

        public async Task<IActionResult> Index()
        {
            var conferences = await _conferenceService.GetConferencesAsync();
            return View(conferences);
        }
    }
}

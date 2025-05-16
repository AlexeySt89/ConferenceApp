using ConferenceApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceApp.Web.Controllers
{
    public class ParticipantsController : Controller
    {
        private readonly IParticipantService _participantService;

        public ParticipantsController(IParticipantService participantService)
        {
            _participantService = participantService;
        }

        public IActionResult Registration() => View();
        public IActionResult ThesisRequirements() => View();
        public IActionResult Submit() => View();
        public IActionResult Fee() => View();
        public IActionResult Accepted()
        {
            var participant = _participantService.GetParticipants();
            return View(participant);
        }
    }
}

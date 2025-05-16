using ConferenceApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceApp.Web.Controllers
{
    public class AboutController : Controller
    {
        private readonly IParProgramCommService _parProgramCommService;
        private readonly IParOrgCommService _parOrgCommService;

        public AboutController(IParProgramCommService parProgramCommService, IParOrgCommService parOrgCommService)
        {
            _parProgramCommService = parProgramCommService;
            _parOrgCommService = parOrgCommService;
        }
        public IActionResult Index() => View();
        public IActionResult Topics() => View();
        public IActionResult OrgCommittee()
        {
            var parOrgComms = _parOrgCommService.GetAllParOrgComm();
            return View(parOrgComms);
        }
        public IActionResult ProgramCommittee()
        {
            var parProgramComms = _parProgramCommService.GetAllParProgramComm();
            return View(parProgramComms);
        }
        public IActionResult News() => View();
        public IActionResult PastMaterials() => View();
        public IActionResult Abstracts() => View();
        public IActionResult Archive() => View();
    }
}
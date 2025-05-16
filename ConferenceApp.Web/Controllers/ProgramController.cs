using Microsoft.AspNetCore.Mvc;

namespace ConferenceApp.Web.Controllers
{
    public class ProgramController : Controller
    {
        public IActionResult Index() => View();
    }
}

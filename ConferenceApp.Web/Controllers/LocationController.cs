using Microsoft.AspNetCore.Mvc;

namespace ConferenceApp.Web.Controllers
{
    public class LocationController : Controller
    {
        public IActionResult HowToGet() => View();
    }
}

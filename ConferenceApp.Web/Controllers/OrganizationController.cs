using Microsoft.AspNetCore.Mvc;

namespace ConferenceApp.Web.Controllers
{
    public class OrganizationController : Controller
    {
        public IActionResult Accommodation() => View();
        public IActionResult Contacts() => View();
    }
}

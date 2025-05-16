using Microsoft.AspNetCore.Mvc;

namespace ConferenceApp.Web.Controllers
{
    public class PostersController : Controller
    {
        public IActionResult Index() => View();
    }
}

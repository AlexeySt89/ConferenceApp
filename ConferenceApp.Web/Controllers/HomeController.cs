﻿using Microsoft.AspNetCore.Mvc;

namespace ConferenceApp.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
    }

}

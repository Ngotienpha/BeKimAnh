using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [Authorize(Roles ="Admin")]
        public IActionResult AdminPage()
        {
            return View();
        }

        [Authorize(Roles ="Student")]
        public IActionResult StudentPage()
        {
            return View();
        }

        [Authorize(Roles = "Manager")]
        public IActionResult ManagerPage()
        {
            return View();
        }

        [Authorize(Roles = "Coordinator 1")]
        public IActionResult Coordinator1()
        {
            return View();
        }
        [Authorize(Roles = "Coordinator 2")]
        public IActionResult Coordinator2()
        {
            return View();
        }
        [Authorize(Roles = "Coordinator 3")]
        public IActionResult Coordinator3()
        {
            return View();
        }
        [Authorize(Roles = "Coordinator 4")]
        public IActionResult Coordinator4()
        {
            return View();
        }
        [Authorize(Roles = "Coordinator 5")]
        public IActionResult Coordinator5()
        {
            return View();
        }
        [Authorize(Roles = "Guest")]
        public IActionResult Guest()
        {
            return View();
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TabCreator.Models;

namespace TabCreator.Controllers
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

        public IActionResult SheetMusic()
        {
            return View();
        }

        public IActionResult Arrangements()
        {
            return View();
        }

        public IActionResult FretboardEditor()
        {
            return View();
        }

        public IActionResult HowTo()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

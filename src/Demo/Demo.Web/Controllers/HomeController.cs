using System.Diagnostics;
using Demo.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //private readonly IEmailUtility _emailUtility;

        public HomeController(ILogger<HomeController> logger)//,[FromKeyedServices("Service1")]IEmailUtility emailUtility)
        {
            _logger = logger;
            //_emailUtility = emailUtility;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
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

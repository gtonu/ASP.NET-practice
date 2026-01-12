using Demo.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Web.Controllers
{
    public class TestController : Controller
    {
        private readonly IEmailUtility _emailUtility;
        //public TestController([FromKeyedServices("Service2")]IEmailUtility emailUtility)
        //{
        //    _emailUtility = emailUtility;
        //}
        public IActionResult Index()
        {
            return View();
        }
    }
}

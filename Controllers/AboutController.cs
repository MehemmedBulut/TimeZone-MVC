using Microsoft.AspNetCore.Mvc;

namespace TimeZoneBack.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

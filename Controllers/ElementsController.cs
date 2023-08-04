using Microsoft.AspNetCore.Mvc;

namespace TimeZoneBack.Controllers
{
    public class ElementsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

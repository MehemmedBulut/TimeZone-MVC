using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TimeZoneBack.DAL;
using TimeZoneBack.ViewModels;

namespace TimeZoneBack.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext _db;
        public ShopController(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            HomeVM homeVM = new HomeVM
            {
                PopularItems = await _db.PopularItems.ToListAsync()
            };
            return View(homeVM);
        }
    }
}

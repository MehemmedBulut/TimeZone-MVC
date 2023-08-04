using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TimeZoneBack.DAL;
using TimeZoneBack.Models;
using TimeZoneBack.ViewModels;

namespace TimeZoneBack.Controllers
{
    public class HomeController : Controller
    {

        private readonly AppDbContext _db;
        public HomeController(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
           
            HomeVM homeVM = new HomeVM
            {
                Arrivals = await _db.Arrivals.Where(x=>x.IsDeactive).Take(3).ToListAsync(),
            PopularItems = await _db.PopularItems.Where(x => x.IsDeactive).OrderByDescending(x=>x.Id).Take(3).ToListAsync()
        };
            return View(homeVM);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

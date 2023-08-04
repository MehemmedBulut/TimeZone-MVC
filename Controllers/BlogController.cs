using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeZoneBack.DAL;
using TimeZoneBack.Models;

namespace TimeZoneBack.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _db;
        public BlogController(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            List<Blog> blog = await _db.Blogs.ToListAsync();
            return View(blog);
        }
    }
}

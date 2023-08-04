using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TimeZoneBack.DAL;
using TimeZoneBack.Models;

namespace TimeZoneBack.Controllers
{
    public class ContactController : Controller
    {
        private readonly AppDbContext _db;
        public ContactController(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            Contact contact = await _db.Contacts.FirstOrDefaultAsync();
            return View(contact);
        }
    }
}

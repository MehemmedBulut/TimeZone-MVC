using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TimeZoneBack.DAL;
using TimeZoneBack.Models;
using TimeZoneBack.ViewModels;

namespace TimeZoneBack.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        private readonly AppDbContext _db;

        public FooterViewComponent(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            FooterVM footerVM = new FooterVM
            {
                Bio = await _db.Bios.FirstOrDefaultAsync(),
                SocialMedia = await _db.SocialMedias.ToListAsync()
            };
            return View(footerVM);
        }
    }
}

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TimeZoneBack.DAL;
using TimeZoneBack.Helpers;
using TimeZoneBack.Models;

namespace TimeZoneBack.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PopularitemsController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;

        public PopularitemsController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<PopularItem> popularItems = await _db.PopularItems.ToListAsync();
            return View(popularItems);
        }

        #region Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PopularItem popular)
        {
            #region Exist
            bool isExist = await _db.PopularItems.AnyAsync(x => x.Name == popular.Name);
            if (isExist)
            {
                ModelState.AddModelError("Name", "this name is already exist");
                return View();
            }
            #endregion
            #region SaveImage
            if (popular.Photo == null)
            {
                ModelState.AddModelError("Photo", "Please select file");
                return View();
            }
            if (!popular.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Please select image file");
                return View();
            }
            if (popular.Photo.IsOlderMb())
            {
                ModelState.AddModelError("Photo", "Max 1mb");
                return View();
            }


            string folder = Path.Combine(_env.WebRootPath, "assets", "img", "gallery");
            popular.Image = await popular.Photo.SaveFileAsync(folder);
            #endregion
            await _db.PopularItems.AddAsync(popular);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion
        #region Update
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            PopularItem dbPopular = await _db.PopularItems.FirstOrDefaultAsync(x => x.Id == id);
            if (dbPopular == null)
            {
                return BadRequest();
            }
            return View(dbPopular);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, PopularItem popular)
        {
            if (id == null)
            {
                return NotFound();
            }
            PopularItem dbPopular = await _db.PopularItems.FirstOrDefaultAsync(x => x.Id == id);
            if (dbPopular == null)
            {
                return BadRequest();
            }
            if (dbPopular.IsDeactive)
            {
                dbPopular.IsDeactive = false;
            }
            else
            {
                dbPopular.IsDeactive = true;
            }

            if (popular.Photo != null)
            {
                if (!popular.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Please select image file");
                    return View();
                }
                if (popular.Photo.IsOlderMb())
                {
                    ModelState.AddModelError("Photo", "Max 1mb");
                    return View();
                }


                string folder = Path.Combine(_env.WebRootPath, "assets", "img", "gallery");
                string fullPath = Path.Combine(folder, dbPopular.Image);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
                dbPopular.Image = await popular.Photo.SaveFileAsync(folder);

            }

            bool isExist = await _db.PopularItems.AnyAsync(x => x.Name == popular.Name);
            if (isExist)
            {
                ModelState.AddModelError("Name", "this name is already exist");
                return View();
            }
            dbPopular.Name = popular.Name;
            dbPopular.Price = popular.Price;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion
        #region Activity
        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            PopularItem popular = await _db.PopularItems.FirstOrDefaultAsync(x => x.Id == id);
            if (popular == null)
            {
                return BadRequest();
            }
            if (popular.IsDeactive)
            {
                popular.IsDeactive = false;
            }
            else
            {
                popular.IsDeactive = true;
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion
        #region Detail
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            PopularItem dbPopular = await _db.PopularItems.FirstOrDefaultAsync(x => x.Id == id);
            if (dbPopular == null)
            {
                return BadRequest();
            }

            return View(dbPopular);
        }
        #endregion
    }
}

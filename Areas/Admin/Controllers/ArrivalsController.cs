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
    public class ArrivalsController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;

        public ArrivalsController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            List<Arrival> arrivals = await _db.Arrivals.ToListAsync();
            return View(arrivals);
        }
        #region Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Arrival arrival)
        {
            #region Exist
            bool isExist = await _db.Arrivals.AnyAsync(x => x.Name == arrival.Name);
            if (isExist)
            {
                ModelState.AddModelError("Name", "this name is already exist");
                return View();
            }
            #endregion
            #region SaveImage
            if (arrival.Photo == null)
            {
                ModelState.AddModelError("Photo", "Please select file");
                return View();
            }
            if (!arrival.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Please select image file");
                return View();
            }
            if (arrival.Photo.IsOlderMb())
            {
                ModelState.AddModelError("Photo", "Max 1mb");
                return View();
            }


            string folder = Path.Combine(_env.WebRootPath, "assets" , "img" ,"gallery");
            arrival.Image = await arrival.Photo.SaveFileAsync(folder);
            #endregion
            await _db.Arrivals.AddAsync(arrival);
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
            Arrival dbArrival = await _db.Arrivals.FirstOrDefaultAsync(x => x.Id == id);
            if (dbArrival == null)
            {
                return BadRequest();
            }
            return View(dbArrival);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Arrival arrival)
        {
            if (id == null)
            {
                return NotFound();
            }
            Arrival dbArrival = await _db.Arrivals.FirstOrDefaultAsync(x => x.Id == id);
            if (dbArrival == null)
            {
                return BadRequest();
            }
            if (dbArrival.IsDeactive)
            {
                dbArrival.IsDeactive = false;
            }
            else
            {
                dbArrival.IsDeactive = true;
            }

            if (arrival.Photo != null)
            {
                if (!arrival.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Please select image file");
                    return View();
                }
                if (arrival.Photo.IsOlderMb())
                {
                    ModelState.AddModelError("Photo", "Max 1mb");
                    return View();
                }


                string folder = Path.Combine(_env.WebRootPath, "assets", "img", "gallery");
                string fullPath = Path.Combine(folder, dbArrival.Image);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
                dbArrival.Image = await arrival.Photo.SaveFileAsync(folder);

            }
            #region Exist
            bool isExist = await _db.Arrivals.AnyAsync(x => x.Name == arrival.Name);
            if (isExist)
            {
                ModelState.AddModelError("Name", "this name is already exist");
                return View();
            }
            #endregion


            dbArrival.Name = arrival.Name;
            dbArrival.Price = arrival.Price;
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
            Arrival arrival = await _db.Arrivals.FirstOrDefaultAsync(x => x.Id == id);
            if (arrival == null)
            {
                return BadRequest();
            }
            if (arrival.IsDeactive)
            {
                arrival.IsDeactive = false;
            }
            else
            {
                arrival.IsDeactive = true;
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
            Arrival dbArrival = await _db.Arrivals.FirstOrDefaultAsync(x => x.Id == id);
            if (dbArrival == null)
            {
                return BadRequest();
            }

            return View(dbArrival);
        }
        #endregion

    }
}

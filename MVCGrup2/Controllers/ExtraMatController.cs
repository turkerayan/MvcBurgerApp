using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCGrup2.Data;
using MVCGrup2.Entities.Concrete;
using MVCGrup2.Models;

namespace MVCGrup2.Controllers
{
    //[Authorize(Roles="Admin")]
    public class ExtraMatController : Controller
    {
        private readonly MVCGrup2Context _context;

        public ExtraMatController(MVCGrup2Context context)
        {
            _context = context;
        }

        // GET: ExtraMat
        public async Task<IActionResult> Index()
        {
            return View(await _context.ExtraMats.ToListAsync());
        }

        // GET: ExtraMat/Details/5
        public async Task<IActionResult> Details(int? id)
        {
           
            if (id == null)
            {
                return NotFound();
            }

            var extraMat = await _context.ExtraMats
                .FirstOrDefaultAsync(m => m.Id == id); 
            ExtraMatModel extraMatModel = new ExtraMatModel()
            {
               Name= extraMat.Name,
               Description= extraMat.Description,
               Active=extraMat.Active,
               Price=extraMat.Price,    
               Size =extraMat.Size, 

            }; 
            if (extraMat == null)
            {
                return NotFound();
            }
            ViewBag.EditedItemId = id; // ID'yi ViewBag ile taşı
            return View(extraMatModel);
        }

        // GET: ExtraMat/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ExtraMat/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Description,Active,Size")] ExtraMatModel extraMatModel)
        {
            if (ModelState.IsValid)
            {
                ExtraMat extraMat = new ExtraMat(extraMatModel.Name, extraMatModel.Price, extraMatModel.Description, extraMatModel.Active, extraMatModel.Size,extraMatModel.Image.FileName);
                if (extraMatModel.Image != null)
                {
                    var fileName = extraMatModel.Image.FileName;

                    var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Resimler");

                    var streamMedia = new FileStream(location, FileMode.Create);

                    extraMatModel.Image.CopyTo(streamMedia);

                    streamMedia.Close();

                    extraMat.ImageName = fileName;

                }
                _context.Add(extraMat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: ExtraMat/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var extraMat = await _context.ExtraMats.FindAsync(id);
            if (extraMat == null)
            {
                return NotFound();
            }
            TempData["id"] = extraMat.Id;

            ExtraMatModel extraMatModel = new ExtraMatModel()
            {
                Name = extraMat.Name,
                Price = extraMat.Price,
                Description = extraMat.Description,
                Active = extraMat.Active,
                Size = extraMat.Size

            };
            return View(extraMatModel);






        }

        // POST: ExtraMat/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Description,Active,Size")] ExtraMatModel extraMatModel)
        {

            if (TempData["id"] == null)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                try
                {
                    ExtraMat ExtraMatUpdate = _context.ExtraMats.FirstOrDefault(u => u.Id == (int)TempData["id"]);

                    if (extraMatModel.Image != null)
                    {
                        var fileName = extraMatModel.Image.FileName;

                        var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Resimler");

                        var streamMedia = new FileStream(location, FileMode.Create);

                        extraMatModel.Image.CopyTo(streamMedia);

                        streamMedia.Close();

                        ExtraMatUpdate.ImageName = fileName;

                    }


                    ExtraMatUpdate.Name = extraMatModel.Name;
                    ExtraMatUpdate.Price = extraMatModel.Price;
                    ExtraMatUpdate.Description = extraMatModel.Description;
                    ExtraMatUpdate.Active = extraMatModel.Active;
                    ExtraMatUpdate.Size = extraMatModel.Size;
                    _context.Update(ExtraMatUpdate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return RedirectToAction(nameof(Index));
                }
                return RedirectToAction(nameof(Index));
            }
            return View(extraMatModel);
        }


        // GET: ExtraMat/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var extraMat = await _context.ExtraMats
                .FirstOrDefaultAsync(m => m.Id == id);
            if (extraMat == null)
            {
                return NotFound();
            }

            return View(extraMat);
        }

        // POST: ExtraMat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var extraMat = await _context.ExtraMats.FindAsync(id);
            if (extraMat != null)
            {
                _context.ExtraMats.Remove(extraMat);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExtraMatExists(int id)
        {
            return _context.ExtraMats.Any(e => e.Id == id);
        }
        public void ResimSil(ExtraMat extraMat)
        {
            var ısImage = _context.ExtraMats.Any(u => u.ImageName == extraMat.ImageName && u.Id != extraMat.Id);
            if (!ısImage)
            {
                var file = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Resimler", extraMat.ImageName);
                System.IO.File.Delete(file);

            }

        }
    }
}

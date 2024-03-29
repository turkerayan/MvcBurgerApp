using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCGrup2.Areas.Admin.Models;
using MVCGrup2.Data;
using MVCGrup2.Entities.Concrete;

namespace MVCGrup2.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
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
			ViewBag.CurrentController = "ExtraMat";
			ViewData["Title"] = "ExtraMat";
			return View(await _context.ExtraMats.ToListAsync());


        }

        // GET: ExtraMat/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var extraMat = await _context.ExtraMats
                .FirstOrDefaultAsync(m => m.Id == id);
            ExtraMatViewModel extraMatModel = new ExtraMatViewModel()
            {
                Id = extraMat.Id,
                Name = extraMat.Name,
                Description = extraMat.Description,
                Active = extraMat.Active,
                ExtraCount = extraMat.ExtraCount,
                Price = extraMat.Price,
                Size = extraMat.Size,
                ImagePath = "\\Pictures\\" + extraMat.PictureName,
            };
            Order order = new Order();


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
        public async Task<IActionResult> Create([Bind("Id,ExtraCount,Name,Price,Description,Active,Size,Image")] ExtraMatViewModel extraMatModel)
        {
            //extraMatModel.ImagePath = " ";
            if (ModelState.IsValid)
            {
                ExtraMat extraMat = new ExtraMat(
                    extraMatModel.Id,
                    extraMatModel.Name,
                    extraMatModel.Price,
                    extraMatModel.Description,
                    extraMatModel.Active,
                    extraMatModel.Size,
                    extraMatModel.Image.FileName);
                extraMat.ExtraCount = extraMatModel.ExtraCount;


                if (extraMatModel.Image != null)
                {
                    var fileName = extraMatModel.Image.FileName;

                    var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Pictures", fileName);

                    var streamMedia = new FileStream(location, FileMode.Create);

                    extraMatModel.Image.CopyTo(streamMedia);

                    streamMedia.Close();

                    extraMat.PictureName = fileName;

                }
                _context.Add(extraMat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: ExtraMat/Edit/5
        // GET: Order/Edit/5

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var extramat = await _context.ExtraMats.FindAsync(id);
            ExtraMatViewModel extraMatViewModel = new ExtraMatViewModel();
            extraMatViewModel.Name = extramat.Name;
            extraMatViewModel.Description = extramat.Description;
            extraMatViewModel.ExtraCount = extramat.ExtraCount;
            extraMatViewModel.Price = extramat.Price;
            extraMatViewModel.Active = extramat.Active;
            extraMatViewModel.ImagePath = "\\Pictures\\" + extramat.PictureName;

            //var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Pictures", extramat.PictureName);
            ViewBag.Extramat = "\\Pictures\\" + extramat.PictureName;

            //FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            //IFormFile file 

            //extraMatViewModel.Image = fileStream.

            //extraMatViewModel.Size = extramat.Size;
            if (extraMatViewModel == null)
            {
                return NotFound();
            }
            return View(extraMatViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,ExtraCount,Name,Price,Description,Active,Size,Image")] ExtraMatViewModel extraMatModel)
        {

            //if (TempData["id"] == null)
            //{
            //    return NotFound();
            //}


            if (ModelState.IsValid)
            {
                try
                {
                    ExtraMat ExtraMatUpdate = _context.ExtraMats.FirstOrDefault(u => u.Id == id);

                    if (extraMatModel.Image != null)
                    {
                        var fileName = extraMatModel.Image.FileName;

                        var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Pictures", fileName);

                        var streamMedia = new FileStream(location, FileMode.Create);

                        extraMatModel.Image.CopyTo(streamMedia);

                        streamMedia.Close();

                        ExtraMatUpdate.PictureName = fileName;

                    }
					else
					{
						extraMatModel.ImagePath = "\\Pictures\\" + ExtraMatUpdate.PictureName;
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
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var extramat = await _context.ExtraMats.FindAsync(id);
            ExtraMatViewModel extraMatViewModel = new ExtraMatViewModel();
            extraMatViewModel.Name = extramat.Name;
            extraMatViewModel.Description = extramat.Description;
            extraMatViewModel.ExtraCount = extramat.ExtraCount;
            extraMatViewModel.Price = extramat.Price;
            extraMatViewModel.Active = extramat.Active;
            extraMatViewModel.ImagePath = "\\Pictures\\" + extramat.PictureName;

            //var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Pictures", extramat.PictureName);
            ViewBag.Extramat = "\\Pictures\\" + extramat.PictureName;
            if (extraMatViewModel == null)
            {
                return NotFound();
            }

            return View(extraMatViewModel);
        }

        // POST: ExtraMat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var extraMat = await _context.ExtraMats.FindAsync(id);
            if (extraMat != null)
            {
                _context.ExtraMats.Remove(extraMat);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExtraMatExists(Guid id)
        {
            return _context.ExtraMats.Any(e => e.Id == id);
        }
    }
}

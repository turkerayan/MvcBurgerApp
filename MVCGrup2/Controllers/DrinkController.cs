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
    public class DrinkController : Controller
    {
        private readonly MVCGrup2Context _context;

        public DrinkController(MVCGrup2Context context)
        {
            _context = context;
        }

        // GET: Drink
        public async Task<IActionResult> Index()
        {
            return View(await _context.Drinks.ToListAsync());
        }

        // GET: Drink/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drink = await _context.Drinks
                .FirstOrDefaultAsync(m => m.Id == id);
            DrinkModel drinkModel = new DrinkModel
            {
                Name = drink.Name,
                Description = drink.Description,
                Price = drink.Price,
                Active = drink.Active,
                Size = drink.Size,
            };
            if (drink == null)
            {
                return NotFound();
            }
            ViewBag.id = id;
            return View(drinkModel);
        }

        // GET: Drink/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Drink/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Description,Active,Size")] DrinkModel drinkModel)
        {
            if (ModelState.IsValid)
            {
                Drink drink = new Drink(
                  drinkModel.Name,
                  drinkModel.Price,
                  drinkModel.Description,
                  drinkModel.Active,
                  drinkModel.Size,
                  drinkModel.Image.FileName
                 
                    );
                if (drinkModel.Image != null)
                {
                    var fileName = drinkModel.Image.FileName;

                    var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Resimler");

                    var streamMedia = new FileStream(location, FileMode.Create);

                    drinkModel.Image.CopyTo(streamMedia);

                    streamMedia.Close();

                    drink.ImageName = fileName;

                }
                _context.Add(drink);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(drinkModel);
        }

        // GET: Drink/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drink = await _context.Drinks.FindAsync(id);
                  
            if (drink == null)
            {
                return NotFound();
            }
            DrinkModel drinkModel = new DrinkModel
            {

                  Name = drink.Name,
                  Price= drink.Price,
                  Description= drink.Description,
                  Active= drink.Active,
                  Size= drink.Size
           };
            TempData["id"]=id;
            return View(drinkModel);
        }

        // POST: Drink/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Description,Active,Size")] DrinkModel drinkModel)
        {
            if (TempData["id"]==null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Drink drinkUpdate=_context.Drinks.FirstOrDefault(d=> d.Id == (int)TempData["id"]);

                    if (drinkModel.Image != null)
                    {
                        var fileName = drinkModel.Image.FileName;

                        var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Resimler");

                        var streamMedia = new FileStream(location, FileMode.Create);

                        drinkModel.Image.CopyTo(streamMedia);

                        streamMedia.Close();

                        drinkUpdate.ImageName = fileName;

                    }




                    drinkUpdate.Name=drinkModel.Name;
                    drinkUpdate.Price=drinkModel.Price;
                    drinkUpdate.Description=drinkModel.Description;
                    drinkUpdate.Active=drinkModel.Active;
                    drinkUpdate.Size=drinkModel.Size;
                    _context.Update(drinkUpdate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                return RedirectToAction(nameof(Index));
                }
              return RedirectToAction(nameof(Index));
            }
            return View(drinkModel);
        }

        // GET: Drink/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drink = await _context.Drinks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (drink == null)
            {
                return NotFound();
            }

            return View(drink);
        }

        // POST: Drink/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var drink = await _context.Drinks.FindAsync(id);
            if (drink != null)
            {
                _context.Drinks.Remove(drink);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DrinkExists(int id)
        {
            return _context.Drinks.Any(e => e.Id == id);
        }
        public void ResimSil(Drink drink)
        {
            var ısImage = _context.Drinks.Any(u => u.ImageName == drink.ImageName && u.Id != drink.Id);
            if (!ısImage)
            {
                var file = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Resimler", drink.ImageName);
                System.IO.File.Delete(file);

            }

        }
    }
}

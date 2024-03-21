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
    //[Authorize(Roles ="Admin")]
    public class BurgerController : Controller
    {
        private readonly MVCGrup2Context _context;

        public BurgerController(MVCGrup2Context context)
        {
            _context = context;
        }

        // GET: Burger
        public async Task<IActionResult> Index()
        {
            return View(await _context.Burgers.ToListAsync());
        }

        // GET: Burger/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var burger = await _context.Burgers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (burger == null)
            {
                return NotFound();
            }
            BurgerModel burgerModel = new BurgerModel
            {
                Name = burger.Name,
                Price = burger.Price,
                Description = burger.Description,
                Active = burger.Active,
                Size = burger.Size
            };

            ViewBag.Id = id;
            return View(burgerModel);
        }

        // GET: Burger/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Burger/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Description,Active,Size")] BurgerModel burgerModel)
        {
            if (ModelState.IsValid)
            {
                Burger burger = new Burger(burgerModel.Name, burgerModel.Price, burgerModel.Description, burgerModel.Active, burgerModel.Size);

                _context.Add(burger);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Burger/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var burger = await _context.Burgers.FindAsync(id);
            if (burger == null)
            {
                return NotFound();
            }
            TempData["id"] = burger.Id;
            BurgerModel burgerModel = new BurgerModel
            {
                Name = burger.Name,
                Price = burger.Price,
                Description = burger.Description,
                Active = burger.Active,
                Size = burger.Size
            };

            return View(burgerModel);
        }

        // POST: Burger/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,Name,Price,Description,Active,Size")] BurgerModel burgerModel)
        {

            if (TempData["id"] == null)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                try
                {
                    Burger burgerUpdate = _context.Burgers.FirstOrDefault(u => u.Id == (int)TempData["id"]);
                    burgerUpdate.Name = burgerModel.Name;
                    burgerUpdate.Price = burgerModel.Price;
                    burgerUpdate.Description = burgerModel.Description;
                    burgerUpdate.Active = burgerModel.Active;
                    burgerUpdate.Size = burgerModel.Size;
                    _context.Update(burgerUpdate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return RedirectToAction(nameof(Index));
                }
                return RedirectToAction(nameof(Index));
            }
            return View(burgerModel);
        }

        // GET: Burger/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var burger = await _context.Burgers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (burger == null)
            {
                return NotFound();
            }

            return View(burger);
        }

        // POST: Burger/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var burger = await _context.Burgers.FindAsync(id);
            if (burger != null)
            {
                _context.Burgers.Remove(burger);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BurgerExists(int id)
        {
            return _context.Burgers.Any(e => e.Id == id);
        }
    }
}

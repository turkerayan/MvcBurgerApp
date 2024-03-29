using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCGrup2.Areas.Admin.Models;
using MVCGrup2.Data;
using MVCGrup2.Entities.Concrete;

namespace MVCGrup2.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class MenuController : Controller
    {
        private readonly MVCGrup2Context _context;

        public MenuController(MVCGrup2Context context)
        {
			
			_context = context;
        }

        // GET: Menu
        public async Task<IActionResult> Index()
        {
			ViewData["Title"] = "Menu";
			ViewBag.CurrentController = "Menu";
			return View(await _context.Menus.ToListAsync());
        }

        // GET: Menu/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = await _context.Menus
                .FirstOrDefaultAsync(m => m.Id == id);
            MenuViewModel menuViewModel = new MenuViewModel()
            {
                Id = menu.Id,
                Name = menu.Name,
                MenuCount = menu.MenuCount,
                Description = menu.Description,
                Active = menu.Active,
                Price = menu.Price,
                Size = menu.Size,
                ImagePath="\\Pictures\\"+menu.PictureName,
            };
            Order order=new Order();

            if (menu == null)
            {
                return NotFound();
            }
            ViewBag.EditedItemId = id; // ID'yi ViewBag ile taşı
            return View(menuViewModel);
        }

        // GET: Menu/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Menu/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,Price,Active,Image")] MenuViewModel menuViewModel)
        {
            if (ModelState.IsValid)
            {
                Menu menu = new Menu(
                menuViewModel.Id,
                menuViewModel.Name,
                menuViewModel.Price,
                menuViewModel.Description,
                menuViewModel.Active,
                menuViewModel.Size,
                menuViewModel.Image.FileName);
                menu.MenuCount = 1;
            menu.Price = menuViewModel.Price;

            if (menuViewModel.Image != null)
            {
                var fileName = menuViewModel.Image.FileName;

                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Pictures", fileName);

                var streamMedia = new FileStream(location, FileMode.Create);

                menuViewModel.Image.CopyTo(streamMedia);

                streamMedia.Close();

                menu.PictureName = fileName;

            }
            _context.Add(menu);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            }
            return View();
        }


        // GET: Menu/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = await _context.Menus.FindAsync(id);
            MenuViewModel menuViewModel=new MenuViewModel();
            menuViewModel.Name = menu.Name;
            menuViewModel .Description = menu.Description;
            menuViewModel.MenuCount = menu.MenuCount;
            menuViewModel.Price=menu.Price;
            menuViewModel.Active = menu.Active;
            menuViewModel.Size = menu.Size;
            menuViewModel.ImagePath="\\Pictures\\" + menu.PictureName;

            ViewBag.ImagePath= "\\Pictures\\" + menu.PictureName;

            if (menuViewModel == null)
            {
                return NotFound();
            }
            return View(menuViewModel);
        }

        // POST: Menu/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,MenuCount,Name,Price,Description,Active,Size,PictureName")] MenuViewModel menuModel)
        {
            //if (id != menu.Id)
            //{
            //    return NotFound();
            //}

            if (ModelState.IsValid)
            {
                try
                {
                    Menu MenuUpdate=_context.Menus.FirstOrDefault(x => x.Id == id);
                    if (menuModel.Image != null)
                    {
                        var fileName = menuModel.Image.FileName;

                        var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Pictures", fileName);

                        var streamMedia = new FileStream(location, FileMode.Create);

                        menuModel.Image.CopyTo(streamMedia);

                        streamMedia.Close();

                        MenuUpdate.PictureName = fileName;

                    }
                    else
                    {
                        menuModel.ImagePath = "\\Pictures\\" + MenuUpdate.PictureName;
					}
                    MenuUpdate.Name= menuModel.Name;
                    MenuUpdate.Price = menuModel.Price;
                    MenuUpdate.Description = menuModel.Description;
                    MenuUpdate.Active = menuModel.Active;
                    MenuUpdate.Size = menuModel.Size;
                    _context.Update(MenuUpdate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return RedirectToAction(nameof(Index));
                }
                return RedirectToAction(nameof(Index));
            }
            return View(menuModel);
        }

        // GET: Menu/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = await _context.Menus.FindAsync(id);
            MenuViewModel menuViewModel= new MenuViewModel();
            menuViewModel.Name = menu.Name;
            menuViewModel.MenuCount = menu.MenuCount;
            menuViewModel.Price=menu.Price;
            menuViewModel.Description=menu.Description;
            menuViewModel.Active = menu.Active;
            menuViewModel.ImagePath = "\\Pictures\\" + menu.PictureName;


            ViewBag.Menu = "\\Pictures\\" + menu.PictureName;

            if (menuViewModel == null)
            {
                return NotFound();
            }
            return View(menuViewModel);
        }

        // POST: Menu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var menu = await _context.Menus.FindAsync(id);
            if (menu != null)
            {
                _context.Menus.Remove(menu);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MenuExists(Guid id)
        {
            return _context.Menus.Any(e => e.Id == id);
        }
    }
}

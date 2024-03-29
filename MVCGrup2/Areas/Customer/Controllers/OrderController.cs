using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCGrup2.Areas.Admin.Models;
using MVCGrup2.Areas.Customer.Models;
using MVCGrup2.Data;
using MVCGrup2.Entities.Concrete;
using MVCGrup2.Models;
using Newtonsoft.Json.Linq;

namespace MVCGrup2.Areas.Customer.Controllers
{
    [Authorize]
    [Area("Customer")]
    public class OrderController : Controller
    {
        private readonly MVCGrup2Context _context;
        private readonly IMapper _mapper;
        private readonly UserManager<MVCGrup2User> _userManager;

        public OrderController(MVCGrup2Context context, IMapper mapper, UserManager<MVCGrup2User> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;

        }

        // GET: Order
        public async Task<IActionResult> Index()
        {
            MVCGrup2User? user = await _userManager.GetUserAsync(User);
            return View(await _context.Orders.Where(u=>u.User.Id == user.Id).ToListAsync());
        }

        // GET: Order/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Order/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Menus = await _context.Menus.ToListAsync();
            ViewBag.Extras = await _context.ExtraMats.ToListAsync();

            return View();
        }

        // POST: Order/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Guid id, [Bind("Id,OrderDate,OrderStatus,OrderCount,Total")] OrderViewModel orderVM, [FromForm] string[] SelectedExtraMats)
        {

            Dictionary<string, int> extraMalzMiktari = new Dictionary<string, int>();

            var formdict = HttpContext.Request.Form.ToDictionary(f => f.Key, f => f.Value.ToString());

            foreach (var extraMatId in SelectedExtraMats)

            {

                extraMalzMiktari.Add(extraMatId, int.Parse(formdict["OrderCounts_" + extraMatId]));

            }

            orderVM.Id = Guid.NewGuid();

            Order order = new Order();

            order = _mapper.Map<Order>(orderVM);

            foreach (var item in extraMalzMiktari)
            {
                var extra = await _context.ExtraMats.Include(o => o.Orders).FirstOrDefaultAsync(e => e.Id == Guid.Parse(item.Key));
                if (extra is ExtraMat)
                {
                    for (int i = 0; i < item.Value; i++)
                    {
                        order.OrderCount++;
                        order.Total += extra.Price;
                        order.ExtraMats.Add(extra);
                    }
                }

                var menu = await _context.Menus.Include(o => o.Orders).FirstOrDefaultAsync(e => e.Id == Guid.Parse(item.Key));
                if (menu is Menu)
                {
                    for (int i = 0; i < item.Value; i++)
                    {
                        order.OrderCount++;
                        order.Total += menu.Price;
                        order.Menus.Add(menu);
                    }
                }
            }

            var userId = _userManager.GetUserId(HttpContext.User);
            var user = _userManager.Users.Include(u => u.Orders).Where(u => u.Id == userId).FirstOrDefault();
            //   order.User = user;
            order.User = user;
            orderVM.User = user;
            //if (ModelState.IsValid)
            //{
            _context.Add(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            //}

            //return View(order);

        }

        // GET: Order/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.Include(e => e.ExtraMats).Include(m => m.Menus).Where(o => o.Id == id).FirstOrDefaultAsync();

            OrderViewModel orderVM = new OrderViewModel();
            orderVM.Id = order.Id;
            orderVM.OrderDate = order.OrderDate;
            orderVM.OrderStatus = order.OrderStatus;
            orderVM.OrderCount = order.OrderCount;
            orderVM.User = order.User;
            //orderVM.ExtraMatsViewModel = (ICollection<ExtraMatViewModel>)order.ExtraMats;
            //orderVM.MenusViewModel = (ICollection<MenuViewModel>)order.Menus;

            orderVM.MenusViewModel = order.Menus.Select(menu =>
                    new MenuViewModel
                    {
                        Id = menu.Id,
                        Name = menu.Name,
                        Description = menu.Description,
                        Active = menu.Active,
                        Size = menu.Size,
                        MenuCount = menu.MenuCount,
                        Price = menu.Price,
                        //OrdersViewModel = (ICollection<OrderViewModel>)menu.Orders,

                    }).ToList();
            orderVM.ExtraMatsViewModel = order.ExtraMats.Select(extra =>
                    new ExtraMatViewModel
                    {
                        Id = extra.Id,
                        Name = extra.Name,
                        Description = extra.Description,
                        Active = extra.Active,
                        Size = extra.Size,
                        ExtraCount = extra.ExtraCount,
                        Price = extra.Price,


                    }).ToList();

            if (orderVM.MenusViewModel != null)
            {
                foreach (var item in orderVM.MenusViewModel)
                {
                    item.ImagePath = "\\Pictures\\" + order.Menus.FirstOrDefault(o => o.Id == item.Id).PictureName;
                }
            }
            if (orderVM.ExtraMatsViewModel != null)
            {
                foreach (var item in orderVM.ExtraMatsViewModel)
                {
                    item.ImagePath = "\\Pictures\\" + order.ExtraMats.FirstOrDefault(o => o.Id == item.Id).PictureName;
                }
            }
            if (orderVM == null)
            {
                return NotFound();
            }
            return View(orderVM);
        }

        // POST: Order/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,OrderDate,OrderStatus,OrderCount,Total")] OrderViewModel orderVM)
        {
            var order = _mapper.Map<Order>(orderVM);






            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Order/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(Guid id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}

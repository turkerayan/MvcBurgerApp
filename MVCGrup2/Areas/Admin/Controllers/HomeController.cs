using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCGrup2.Data;
using MVCGrup2.Enums;

namespace MVCGrup2.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private readonly MVCGrup2Context _context;
        private readonly UserManager<MVCGrup2User> _userManager;

        public HomeController(MVCGrup2Context context, UserManager<MVCGrup2User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            MVCGrup2User? user = await _userManager.GetUserAsync(User);

            ViewData["User"] = user?.Name;
			ViewBag.CurrentController = "Home";
            return View(await _context.Orders.OrderBy(o=>o.OrderStatus).ThenByDescending(o=>o.OrderDate).ToListAsync());

        }

        public async Task<IActionResult> ShowOnlyActives()
        {
            return View(await _context.Orders.Where(o => o.OrderStatus != OrderStatus.Cancelled && o.OrderStatus != OrderStatus.Delivered).OrderByDescending(o=>o.OrderDate).ToListAsync());
        }

        public async Task<IActionResult> ShowOnlyDelivered()
        {
            return View(await _context.Orders.Where(o => o.OrderStatus == OrderStatus.Delivered).OrderByDescending(o => o.OrderDate).ToListAsync());
        }

        public async Task<IActionResult> Manage(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                    .Include(o => o.User)
                    .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        public async Task<IActionResult> SetOrderStatus(Guid? id, OrderStatus status)
        {
            var order = await _context.Orders
                    .Include(o => o.User)
                    .FirstOrDefaultAsync(o => o.Id == id);

            order.OrderStatus = status;
            _context.SaveChanges();
            return RedirectToAction("Manage", new { id = order.Id });
        }}
}

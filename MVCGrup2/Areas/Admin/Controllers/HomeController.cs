using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCGrup2.Data;

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

            ViewBag.Orders = "";
			ViewData["Title"] = "Home";
			ViewBag.CurrentController = "Home";
            return View(await _context.Orders.ToListAsync());
			
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using MVCGrup2.Data;
using MVCGrup2.Models;
using System.Diagnostics;

namespace MVCGrup2.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly MVCGrup2Context _db;
        public HomeController(ILogger<HomeController> logger,MVCGrup2Context db)
		{
			_logger = logger;
			_db = db;
		}

        public IActionResult Index()
        {
            return View(_db.Menus.ToList());
        }


 
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}

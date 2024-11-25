using Microsoft.AspNetCore.Mvc;
using SignalR.Web.Models;
using System.Diagnostics;

namespace SignalR.Web.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}
		public IActionResult Stream()
		{
			//bizim datalarimizin hacmi buyuk olabilir o zaman veriyi parcalayarak (chunk) ornegin 4 parca halinde clienttan hub'a gonderebiliriz buna stream denir
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}

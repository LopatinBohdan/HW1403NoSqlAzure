using HW1403NoSql.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HW1403NoSql.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
       // MyContext db;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
           // db = new MyContext();
           // db.Database.EnsureCreated();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
using JewelyShop.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
//Added by us
using System.Data.Entity;

namespace JewelyShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(int? id)
        {
            if(id == null)
            {
                Group group = Datalayer.Data.GroupsAllIncluded.First();
                List<Item> items = group.AllItems;
                return View(items);
            }
            else
            {
                Group group = Datalayer.Data.GroupsAllIncluded.Find(g=> g.ID == id);
                List<Item> items = group.AllItems;
                return View(items);
            }
          
        }

        public IActionResult test()
        {

            return View(new Price());
        }
        [HttpPost]
        public IActionResult test(Price price)
        {

            return View(price);
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
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
            // קבלת כל הקבוצות מהמסד נתונים
            List<Group> groups = Datalayer.Data.GroupsAllIncluded;
            // בדיקה האם לא התקבל קוד, מחזיר את הקבוצה הראשית
            if (id == null) return View(groups.First().AllItems);
            // מחפש את הקבוצה לפי הקוד שהתקבל בפונקציה
            Group group = groups.Find(g=> g.ID == id);
            // אם לא נמצאה קבוצה, מחזיר את הקבוצה הראשית
            if (group == null) return View(groups.First().AllItems);
            // מחזיר את הקבוצה שנמצאה
            return View(group.AllItems);
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
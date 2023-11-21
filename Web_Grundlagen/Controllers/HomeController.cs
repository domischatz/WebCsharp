using Microsoft.AspNetCore.Mvc;

namespace Web_Grundlagen.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AboutUs()
        {
            return View() ;// rechtsklich ansicht hinzufügen
        }
    }
}

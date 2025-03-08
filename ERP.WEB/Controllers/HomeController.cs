using Microsoft.AspNetCore.Mvc;

namespace ERP.WEB.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

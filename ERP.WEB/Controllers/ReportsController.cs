using ERP.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ERP.WEB.Controllers
{
    public class ReportsController : Controller
    { 
        public IActionResult Index()
        {
            return View();
        }
    }
}

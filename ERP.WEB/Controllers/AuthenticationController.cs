using Microsoft.AspNetCore.Mvc;

namespace ERP.WEB.Controllers
{

    public class AuthenticationController : Controller
    {
        public ActionResult Registration()
        {
            return View();
        }
    }
}

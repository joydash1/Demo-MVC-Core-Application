using ERP.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ERP.WEB.Controllers
{
    public class ProductLedgerController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISpService _spService;

        public ProductLedgerController(IUnitOfWork unitOfWork, ISpService spService)
        {
            _unitOfWork = unitOfWork;
            _spService = spService;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
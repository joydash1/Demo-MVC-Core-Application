using ERP.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ERP.WEB.Controllers
{
    public class ProductStockController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISpService _spService;

        public ProductStockController(IUnitOfWork unitOfWork, ISpService spService)
        {
            _unitOfWork = unitOfWork;
            _spService = spService;
        }

        #region Stock
        public IActionResult Stock()
        {
            return View();
        } 
        #endregion
    }
}

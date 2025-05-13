using ERP.DataAccess.DTOs.Basic_Setup;
using ERP.Infrastructure.Interfaces;
using ERP.Utility.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ERP.WEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISpService _spService;
        private readonly ReportService _reportService;

        public HomeController(IUnitOfWork unitOfWork, ISpService spService, ReportService reportService)
        {
            _unitOfWork = unitOfWork;
            _spService = spService;
            _reportService = reportService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ShowReport()
        {
            var productlist = await _spService.GetDataWithParameterAsync<ProductDto>(new { ID = 0 }, "USP_GET_PRODUCT_LIST").ToListAsync();
            var data = _reportService.ConvertToDataTable(productlist);
            var reportTitle = new Dictionary<string, string>
            {
                { "Title", "Product List" },
                { "Date", DateTime.Now.ToString("dd/MM/yyyy") }
                //{ "Duration", $"{fromDate} To {toDate}" },
            };
            return _reportService.ShowReport(data, "PDF", "rptProduct.rdlc", "Product Report", reportTitle);
        }
    }
}
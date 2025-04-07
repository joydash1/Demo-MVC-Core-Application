using ERP.DataAccess.Domains;
using ERP.DataAccess.DTOs.BankBranch;
using ERP.DataAccess.DTOs.Organization;
using ERP.Infrastructure.Interfaces;
using ERP.Utility.Helpers;
using ERP.Utility.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
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
            var bankbranch = await _spService.GetDataWithoutParameterAsync<dynamic>("USP_GET_APP_USER").ToListAsync();
            var data = _reportService.ConvertToDataTable(bankbranch);
            
            var reportTitle = new Dictionary<string, string>
            {
                { "Title", "My First Report" },
                { "Date", DateTime.Now.ToString("dd/MM/yyyy") }
                //{ "EndDate", "2024-12-31" },
            };
            return _reportService.ShowReport(data, "PDF","SampleReport.rdlc", "Sample Report", reportTitle);
        }
    }
}
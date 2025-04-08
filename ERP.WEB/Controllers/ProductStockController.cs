using ERP.DataAccess.DTOs.LC_Open;
using ERP.Infrastructure.Interfaces;
using ERP.Utility.Helpers;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        public async Task<IActionResult> Stock()
        {
            ViewBag.LCFileList = await _spService.GetDataWithoutParameterAsync<LCTransactionListDto>("USP_GET_LC_OPENING_TRANSACTION_LIST").ToListAsync();

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> GetLCTransactionInfoById(int Id)
        {
            try
            {
                var data = await _spService.GetDataWithParameterAsync<LCTransactionListDto>(new
                {
                    ID = Id
                }, "USP_GET_LC_OPENING_TRANSACTION_LIST");

                return Json(new
                {
                    Status = true,
                    Data = data
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Status = false,
                    Message = ex.GetBaseException()
                });
            }
        }
        #endregion
    }
}

using DocumentFormat.OpenXml.Bibliography;
using ERP.DataAccess.DTOs.BankBranch;
using ERP.DataAccess.DTOs.Basic_Setup;
using ERP.DataAccess.DTOs.Buyer;
using ERP.DataAccess.DTOs.CMF;
using ERP.DataAccess.DTOs.LC_Open;
using ERP.Infrastructure.Interfaces;
using ERP.Utility.Helpers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ERP.WEB.Controllers
{
    public class CMFFilesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISpService _spService;

        public CMFFilesController(IUnitOfWork unitOfWork, ISpService spService)
        {
            _unitOfWork = unitOfWork;
            _spService = spService;
        }
        #region CMF File Open

        public async Task<IActionResult> CMFFile()
        {
            ViewBag.LCFileList = await _unitOfWork.LCFileRepository.GetAllAsync(x => x.IsActive == true);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> GetLCTransactionInfoById(int Id)
        {
            try
            {
                var data = await _spService.GetDataWithParameterAsync<LCTransactionsByIdDto>(new
                {
                    ID = Id
                }, "USP_GET_LC_OPENING_TRANSACTION_BY_ID");

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

        [HttpPost]
        public async Task<IActionResult> SaveCustomDataRecord([FromForm]  CustomDataRecordAndStockDto customDataRecordDto, [FromForm] string BillingData)
        {
            try
            {
                if (customDataRecordDto == null)
                {
                    TempData["AlertMessage"] = "Invalid Data.";
                    TempData["AlertType"] = "error";
                    return RedirectToAction("CMFFile");
                }

                var data = await _spService.GetDataWithParameterAsync<InsertResultDto>(new
                {
                    LC_ID = customDataRecordDto.LCId,
                    USD_RATE_IN_TAKA = customDataRecordDto.USDRateToTaka,
                    TOTAL_PRODUCT_WEIGHT = customDataRecordDto.TotalProductWeightKg,
                    TOTAL_PRODUCT_PRICE = customDataRecordDto.TotalProductPrice,
                    PRICE_PER_KG = customDataRecordDto.ProductPricePerKg,
                    TRUCK_NUMBER = customDataRecordDto.TruckNumber,
                    USER_ID = SessionHelper.GetLoggedInUserId(HttpContext)
                }, "USP_INSERT_CUSTOM_DATA_RECORD_AND_STOCK");

                int NewSaveId = data.FirstOrDefault()?.NewRecordId ?? 0;

                var billingList = JsonConvert.DeserializeObject<List<CustomDataRecordAndStockBillingDto>>(BillingData);


                foreach (var item in billingList)
                {
                    await _spService.GetDataWithParameterAsync<dynamic>(new
                        {
                            CMF_ID = NewSaveId,
                            EXPENSE_DETAILS = item.ExpenseDetails.Trim(),
                            EXPENSE_AMOUNT = item.ExpenseAmount,
                            USER_ID = SessionHelper.GetLoggedInUserId(HttpContext)
                        }, "USP_INSERT_CUSTOM_DATA_RECORD_BILLING");
                }

                string message = "CMF Save successfully";

                return Json(new
                {
                    Status = true,
                    Message = message
                });
            }
            catch (Exception ex)
            {
                TempData["AlertMessage"] = "An error occurred. Please try again.";
                TempData["AlertType"] = "error";
                return RedirectToAction("CMFFile");
            }
        }
        #endregion
    }
}

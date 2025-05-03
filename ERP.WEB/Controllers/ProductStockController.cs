using ERP.DataAccess.DTOs.LC_Open;
using ERP.DataAccess.DTOs.Product_Stock;
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

        #region Product Stock & CNF File Opening
        public async Task<IActionResult> Stock()
        {
            ViewBag.LCFileList = await _unitOfWork.LCFileRepository.GetAllAsync();
            return View();
        }
        //[HttpPost]
        //public async Task<IActionResult> InsertUpdateProductStock(ProductStockDto productStockDto)
        //{
        //    try
        //    {
        //        if (productStockDto == null)
        //        {
        //            TempData["AlertMessage"] = "Invalid Data.";
        //            TempData["AlertType"] = "error";
        //            return RedirectToAction("LCTransactionList");
        //        }

        //        var data = await _spService.GetDataWithParameterAsync<ProductStockDto>(new
        //        {
        //            ID = productStockDto.Id,
        //            LCId = productStockDto.lcId,
        //            ProductWeightKg = productStockDto.productWeightKg,
        //            USDRate = productStockDto.uSDRate,
        //            TotalProductPrice = productStockDto.totalProductPrice,
        //            PricePerkg = productStockDto.pricePerkg,
        //            TruckNo = productStockDto.truckNo,
        //            TotalBags = productStockDto.totalBags,
        //            UserId = SessionHelper.GetLoggedInUserId(HttpContext)
        //        }, "USP_INSERT_UPDATE_PRODUCT_STOCK");

        //        string message = productStockDto.Id > 0 ? "Product Stock Updated Successfully" : "Product Stock Save Successfully";

        //        return Json(new
        //        {
        //            Status = true,
        //            Message = message
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["AlertMessage"] = "An error occurred. Please try again.";
        //        TempData["AlertType"] = "error";
        //        return RedirectToAction("Stock");
        //    }
        //}

        [HttpPost]
        public async Task<IActionResult> GetLCTransactionById(LCTransactionDto get)
        {
            try
            {
                if (get.Id == 0)
                {
                    TempData["AlertMessage"] = "Invalid Data.";
                    TempData["AlertType"] = "error";
                    return RedirectToAction("LCFile", "LCFiles");
                }

                var data = await _spService.GetDataWithParameterAsync<dynamic>(new
                {
                    ID = get.Id
                }, "USP_GET_LC_TRANSACTION_BY_ID");

                return Json(new
                {
                    Status = true,
                    Data = data
                });
            }
            catch (Exception ex)
            {
                TempData["AlertMessage"] = "An error occurred. Please try again.";
                TempData["AlertType"] = "error";
                return RedirectToAction("LCFile", "LCFiles");
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteLCTransactionById(LCTransactionDto delete)
        {
            try
            {
                if (delete.Id == 0)
                {
                    TempData["AlertMessage"] = "Invalid Data.";
                    TempData["AlertType"] = "error";
                    return RedirectToAction("LCFile");
                }

                var data = await _spService.GetDataWithParameterAsync<LCTransactionDto>(new
                {
                    ID = delete.Id,
                    USER_ID = SessionHelper.GetLoggedInUserId(HttpContext)
                }, "USP_DELETE_LC_TRANSACTION_BY_ID");

                return Json(new
                {
                    Status = true,
                    Message = "LC File deleted successfully"
                });
            }
            catch (Exception ex)
            {
                TempData["AlertMessage"] = "An error occurred. Please try again.";
                TempData["AlertType"] = "error";
                return RedirectToAction("LCFile");
            }
        }
        #endregion
    }
}

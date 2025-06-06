using ERP.DataAccess.Domains;
using ERP.DataAccess.DTOs.APIResponses;
using ERP.DataAccess.DTOs.Basic_Setup;
using ERP.DataAccess.DTOs.ClearingAndFordwingCNF;
using ERP.DataAccess.DTOs.LC_Open;
using ERP.DataAccess.DTOs.ProductStock;
using ERP.Infrastructure.Interfaces;
using ERP.Utility.Helpers;
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

        #region Product Stock

        public async Task<IActionResult> Stock()
        {
            ViewBag.LCFileList = await _unitOfWork.LCFileRepository.GetAllAsync();
            return View();
        }

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
        public async Task<IActionResult> SaveUpdateProductStock(ProductStockMasterDto save)
        {
            try
            {
                if (save == null)
                {
                    TempData["AlertMessage"] = "Invalid Data.";
                    TempData["AlertType"] = "error";
                    return RedirectToAction("Stock");
                }
                var data = await _spService.GetDataWithParameterAsync<ProductStockMasterDto>(new
                {
                    ID = save.Id,
                    LC_ID = save.LCId,
                    STOCK_IN_TON = save.TotalStockInQuantityTon,
                    STOCK_IN_KG = save.TotalStockInQuantityKg,
                    PRICE_PER_KG_TK = save.StockPricePerKg,
                    TOTAL_PRICE_TK = save.StockTotalPriceTk,
                    USER_ID = SessionHelper.GetLoggedInUserId(HttpContext)
                }, "USP_INSERT_UPDATE_PRODUCT_STOCK");

                string message = save.Id > 0 ? "Product Stock Updated Successfully" : "Product Stock Saved Successfully";

                var result = new ResponseResult
                {
                    Status = true,
                    Message = message
                };
                return Json(result);
            }
            catch (Exception ex)
            {
                var result = new ResponseResult
                {
                    Status = false,
                    Message = "An error occurred. Please try again."
                };
                return Json(result);
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetProductStockList()
        {
            try
            {
                var data = await _spService.GetDataWithoutParameterAsync<ProductStockListDto>("USP_GET_PRODUCT_STOCK_LIST");

                return Json(new ResponseListResult<List<ProductStockListDto>>
                {
                    Status = true,
                    Data = (List<ProductStockListDto>)data
                });
            }
            catch (Exception ex)
            {
                TempData["AlertMessage"] = "An error occurred. Please try again.";
                TempData["AlertType"] = "error";
                return RedirectToAction("Stock");
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProductStockById(ProductStockMasterDto delete)
        {
            try
            {
                if (delete.Id == 0)
                {
                    TempData["AlertMessage"] = "Invalid Data.";
                    TempData["AlertType"] = "error";
                    return RedirectToAction("Stock");
                }

                var data = await _spService.GetDataWithParameterAsync<ProductStockMasterDto>(new
                {
                    ID = delete.Id,
                    USER_ID = SessionHelper.GetLoggedInUserId(HttpContext)
                }, "USP_DELETE_PRODUCT_STOCK_BY_ID");

                var result = new ResponseResult
                {
                    Status = true,
                    Message = "Product Stock Deleted Successfully."
                };
                return Json(result);
            }
            catch (Exception ex)
            {
                TempData["AlertMessage"] = "An error occurred. Please try again.";
                TempData["AlertType"] = "error";
                return RedirectToAction("Stock");
            }
        }

        #endregion Product Stock
    }
}
using ERP.DataAccess.DTOs.BankBranch;
using ERP.DataAccess.DTOs.Basic_Setup;
using ERP.DataAccess.DTOs.Buyer;
using ERP.DataAccess.DTOs.LC_Open;
using ERP.Infrastructure.Interfaces;
using ERP.Utility.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;

namespace ERP.WEB.Controllers
{
    public class LCFilesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISpService _spService;

        public LCFilesController(IUnitOfWork unitOfWork, ISpService spService)
        {
            _unitOfWork = unitOfWork;
            _spService = spService;
        }
        #region LC File Open

        public async Task<IActionResult> LCFile()
        {
            //DropDown
            ViewBag.Organization = await _unitOfWork.OrganizationRepository.GetAllAsync(x => x.IsActive == true);
            ViewBag.ProductList = await _spService.GetDataWithParameterAsync<ProductDto>(new { ID = 0 }, "USP_GET_PRODUCT_LIST").ToListAsync();
            ViewBag.OrganizationBankAccountList = await _spService.GetDataWithoutParameterAsync<OrganizationAccountListDto>("USP_GET_ORGANIZATION_BANK_ACCOUNT_LIST").ToListAsync();
            ViewBag.InsuranceList = await _spService.GetDataWithParameterAsync<InsuranceDto>(new { ID = 0 }, "USP_GET_INSURANCE_LIST").ToListAsync();
            ViewBag.BorderList = await _spService.GetDataWithParameterAsync<BorderDto>(new { ID = 0 }, "USP_GET_BORDER_LIST").ToListAsync();
            ViewBag.IndianBuyerList = await _spService.GetDataWithParameterAsync<IndianBuyerDto>(new { ID = 0 }, "USP_GET_INDIAN_BUYER_LIST").ToListAsync();
            //DropDown
            ViewBag.LCFileList = await _spService.GetDataWithoutParameterAsync<LCTransactionListDto>("USP_GET_LC_OPENING_TRANSACTION_LIST").ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> InsertUpdateLCTransaction(LCTransactionDto lcTransaction)
        {
            try
            {
                if (lcTransaction == null)
                {
                    TempData["AlertMessage"] = "Invalid Data.";
                    TempData["AlertType"] = "error";
                    return RedirectToAction("LCTransactionList");
                }

                var data = await _spService.GetDataWithParameterAsync<LCTransactionDto>(new
                {
                    ID = lcTransaction.Id,
                    OPENING_DATE = ReportHelpers.FormatDateToString(lcTransaction.OpeningDate),
                    BANK_ID = lcTransaction.OrganizationBankId,
                    BRANCH_ID = lcTransaction.OrganizationBranchId,
                    ACCOUNT_NUMBER = lcTransaction.OrganizationAccountNo,
                    INSURANCE_ID = lcTransaction.InsuranceId,
                    LC_NUMBER = lcTransaction.LCNumber,
                    PRODUCT_WEIGHT_TON = lcTransaction.ProductWeightTon,
                    AMOUNT_PER_TON = lcTransaction.AmountPerTon,
                    TOTAL_LC_AMOUNT = lcTransaction.TotalLCAmount,
                    PRODUCT_ID = lcTransaction.ProductId,
                    BORDER_ID = lcTransaction.BorderId,
                    IMPORTER_ID = lcTransaction.ImporterId,
                    EXPORTER_ID = lcTransaction.ExporterId,
                    USER_ID = SessionHelper.GetLoggedInUserId(HttpContext)
                }, "USP_INSERT_UPDATE_LC_TRANSACTION");

                string message = lcTransaction.Id > 0 ? "LC File updated successfully" : "LC File open successfully";

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
                return RedirectToAction("LCFile");
            }
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

        [HttpPost]
        public async Task<IActionResult> GetOrganizationBankAccByBankId(OrganizationAccountNoDto get)
        {
            try
            {
                if (get.Id == 0)
                {
                    TempData["AlertMessage"] = "Invalid Data.";
                    TempData["AlertType"] = "error";
                    return RedirectToAction("LCFile", "LCFiles");
                }

                var data = await _spService.GetDataWithParameterAsync<OrganizationAccountNoDto>(new
                {
                    BANK_ID = get.BankId
                }, "USP_GET_ORGANIZATION_BANK_ACCOUNT_AND_BRANCH_BY_BANKID");

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

        #endregion
    }
}

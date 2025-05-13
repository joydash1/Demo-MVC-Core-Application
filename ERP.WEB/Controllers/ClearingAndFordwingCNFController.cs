using ERP.DataAccess.Domains;
using ERP.DataAccess.DTOs.APIResponses;
using ERP.DataAccess.DTOs.ClearingAndFordwingCNF;
using ERP.DataAccess.DTOs.LC_Open;
using ERP.Infrastructure.Interfaces;
using ERP.Utility.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ERP.WEB.Controllers
{
    public class ClearingAndFordwingCNFController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISpService _spService;

        public ClearingAndFordwingCNFController(IUnitOfWork unitOfWork, ISpService spService)
        {
            _unitOfWork = unitOfWork;
            _spService = spService;
        }

        #region Clearing & Fordwing CNF

        public async Task<IActionResult> Index()
        {
            ViewBag.LCFileList = await _unitOfWork.LCFileRepository.GetAllAsync(x => x.IsActive == true);
            ViewBag.CNFCompanyList = await _unitOfWork.CNFCompanyRepository.GetAllAsync(x => x.IsActive == true);
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
        public async Task<IActionResult> SaveUpdateClearingAndFordwingCNF(ClearingAndForwardingCNFDto save)
        {
            try
            {
                if (save == null)
                {
                    TempData["AlertMessage"] = "Invalid Data.";
                    TempData["AlertType"] = "error";
                    return RedirectToAction("Index");
                }

                var data = await _spService.GetDataWithParameterAsync<ClearingAndForwardingCNFDto>(new
                {
                    ID = save.Id,
                    LC_ID = save.LCId,
                    CNF_COMPANY_ID = save.CNFCompanyId,
                    CNF_WEIGHT = save.CNFWeight,
                    USER_ID = SessionHelper.GetLoggedInUserId(HttpContext)
                }, "USP_INSERT_UPDATE_CLEARING_AND_FORWARDING_CNF");

                string message = save.Id > 0 ? "Clearing & Fordwing Updated Successfully" : "Clearing & Fordwing Saved Successfully";

                var result = new ResponseResult
                {
                    Status = true,
                    Message = message
                };
                return Json(result);
            }
            catch (Exception ex)
            {
                TempData["AlertMessage"] = "An error occurred. Please try again.";
                TempData["AlertType"] = "error";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> ClearingAndFordwingList(ClearingAndForwardingCNFDto get)
        {
            try
            {
                var data = await _spService.GetDataWithParameterAsync<ClearingAndForwardingCNFDto>(new
                {
                    LC_ID = get.LCId
                }, "USP_GET_CLEARING_AND_FORWARDING_BY_LC_ID");

                return Json(new ResponseListResult<List<ClearingAndForwardingCNFDto>>
                {
                    Status = true,
                    Data = (List<ClearingAndForwardingCNFDto>)data
                });
            }
            catch (Exception ex)
            {
                TempData["AlertMessage"] = "An error occurred. Please try again.";
                TempData["AlertType"] = "error";
                return RedirectToAction("Index", "ClearingAndFordwingCNF");
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteClearingAndFordwingById(ClearingAndForwardingCNFDto delete)
        {
            try
            {
                if (delete.Id == 0)
                {
                    TempData["AlertMessage"] = "Invalid Data.";
                    TempData["AlertType"] = "error";
                    return RedirectToAction("Index");
                }

                var data = await _spService.GetDataWithParameterAsync<ClearingAndForwardingCNFDto>(new
                {
                    ID = delete.Id,
                    USER_ID = SessionHelper.GetLoggedInUserId(HttpContext)
                }, "USP_DELETE_CLEARING_AND_FORWARDING_BY_ID");

                var result = new ResponseResult
                {
                    Status = true,
                    Message = "Clearing & Forwarding (CNF) deleted successfully"
                };
                return Json(result);
            }
            catch (Exception ex)
            {
                TempData["AlertMessage"] = "An error occurred. Please try again.";
                TempData["AlertType"] = "error";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetClearingAndFordwingById(ClearingAndForwardingCNFDto edit)
        {
            try
            {
                if (edit.Id == 0)
                {
                    TempData["AlertMessage"] = "Invalid Data.";
                    TempData["AlertType"] = "error";
                    return RedirectToAction("Index");
                }

                var data = await _spService.GetDataWithParameterAsync<ClearingAndForwardingCNFDto>(new
                {
                    ID = edit.Id
                }, "USP_GET_CLEARING_AND_FORWARDING_BY_ID_FOR_EDIT");

                return Json(new ResponseListResult<List<ClearingAndForwardingCNFDto>>
                {
                    Status = true,
                    Data = data.ToList()
                });
            }
            catch (Exception ex)
            {
                TempData["AlertMessage"] = "An error occurred. Please try again.";
                TempData["AlertType"] = "error";
                return RedirectToAction("Index");
            }
        }

        #endregion Clearing & Fordwing CNF

        #region CNF Payment

        public async Task<IActionResult> CNFPayment()
        {
            ViewBag.LCFileList = await _unitOfWork.LCFileRepository.GetAllAsync(x => x.IsActive == true);
            ViewBag.CollectionModeList = await _unitOfWork.CollectionModeRepository.GetAllAsync(x => x.IsActive == 1);
            return View();
        }

        #endregion CNF Payment
    }
}
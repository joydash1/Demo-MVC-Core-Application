using ERP.DataAccess.Domains;
using ERP.DataAccess.DTOs.APIResponses;
using ERP.DataAccess.DTOs.BankBranch;
using ERP.DataAccess.DTOs.ClearingAndFordwingCNF;
using ERP.DataAccess.DTOs.ClearingAndForwardingCNF;
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

        //[HttpPost]
        //public async Task<IActionResult> SaveUpdateClearingAndFordwingCNF(ClearingAndForwardingCNFDto save)
        //{
        //    try
        //    {
        //        if (save == null)
        //        {
        //            TempData["AlertMessage"] = "Invalid Data.";
        //            TempData["AlertType"] = "error";
        //            return RedirectToAction("Index");
        //        }
        //        // File upload Part
        //        string? filePath = null;

        //        if (save.File != null && save.LCNumber != null)
        //        {
        //            var ext = Path.GetExtension(save.File.FileName);
        //            var fileName = Guid.NewGuid().ToString() + ext;
        //            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "CNFDocuments", save.LCNumber);

        //            if (!Directory.Exists(folderPath))
        //                Directory.CreateDirectory(folderPath);

        //            filePath = Path.Combine(folderPath, fileName);
        //            using (var stream = new FileStream(filePath, FileMode.Create))
        //            {
        //                await save.File.CopyToAsync(stream);
        //            }
        //            save.CNFDocumentFile = Path.Combine("/CNFDocuments", save.LCNumber, fileName).Replace("\\", "/");
        //        }
        //        //End
        //        var data = await _spService.GetDataWithParameterAsync<ClearingAndForwardingCNFDto>(new
        //        {
        //            ID = save.Id,
        //            LC_ID = save.LCId,
        //            CNF_COMPANY_ID = save.CNFCompanyId,
        //            CNF_WEIGHT = save.CNFWeight,
        //            CNF_AMOUNT = save.CNFAmount,
        //            CNF_DOCUMENT = save.CNFDocumentFile ?? "",
        //            USER_ID = SessionHelper.GetLoggedInUserId(HttpContext)
        //        }, "USP_INSERT_UPDATE_CLEARING_AND_FORWARDING_CNF");

        //        string message = save.Id > 0 ? "Clearing & Fordwing Updated Successfully" : "Clearing & Fordwing Saved Successfully";

        //        var result = new ResponseResult
        //        {
        //            Status = true,
        //            Message = message
        //        };
        //        return Json(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["AlertMessage"] = "An error occurred. Please try again.";
        //        TempData["AlertType"] = "error";
        //        return RedirectToAction("Index");
        //    }
        //}

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

                // File upload Part
                string? filePath = null;

                if (save.File != null && save.LCNumber != null)
                {
                    var ext = Path.GetExtension(save.File.FileName);
                    var fileName = Guid.NewGuid().ToString() + ext;
                    var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "CNFDocuments", save.LCNumber);

                    if (!Directory.Exists(folderPath))
                        Directory.CreateDirectory(folderPath);

                    filePath = Path.Combine(folderPath, fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await save.File.CopyToAsync(stream);
                    }
                    save.CNFDocumentFile = Path.Combine("/CNFDocuments", save.LCNumber, fileName).Replace("\\", "/");
                }
                //End

                try
                {
                    var data = await _spService.GetDataWithParameterAsync<ClearingAndForwardingCNFDto>(new
                    {
                        ID = save.Id,
                        LC_ID = save.LCId,
                        CNF_COMPANY_ID = save.CNFCompanyId,
                        CNF_WEIGHT = save.CNFWeight,
                        CNF_AMOUNT = save.CNFAmount,
                        CNF_DOCUMENT = save.CNFDocumentFile ?? "",
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
                catch (Exception ex) when (ex.Message.Contains("Cannot update CNF record because payment has already been made"))
                {
                    var result = new ResponseResult
                    {
                        Status = false,
                        Message = ex.Message
                    };
                    return Json(result);
                }
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
            ViewBag.OrganizationBankAccountList = await _spService.GetDataWithoutParameterAsync<OrganizationAccountListDto>("USP_GET_ORGANIZATION_BANK_ACCOUNT_LIST").ToListAsync();
            ViewBag.CNFCompanyList = await _unitOfWork.CNFCompanyRepository.GetAllAsync(x => x.IsActive == true);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetNonPaidCnfPaymentList([FromBody] NonPaidCNFPaymentListDtos get)
        {
            try
            {
                var data = await _spService.GetDataWithParameterAsync<NonPaidCNFPaymentListDtos>(new
                {
                    CNF_COMPANY_ID = get.ID
                }, "USP_GET_DUE_CNF_PAYMENT_LIST_BY_CNF_COMPANY");

                return Json(new ResponseListResult<List<NonPaidCNFPaymentListDtos>>
                {
                    Status = true,
                    Data = (List<NonPaidCNFPaymentListDtos>)data
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
        public async Task<IActionResult> SaveCNFPayments([FromBody] List<CNFPaymentSaveDto> payments)
        {
            try
            {
                foreach (var item in payments)
                {
                    await _spService.GetDataWithParameterAsync<CNFPaymentSaveDto>(new
                    {
                        CNF_PAYMENT_ID = item.CNFPaymentId,
                        PAID_AMOUNT = item.PaidAmount,
                        DUE_AMOUNT = item.DueAmount,
                        PAYMENT_DATE = ReportHelpers.TryParseDate(item.PaymentDate),
                        COLLECTION_MODE_ID = item.CollectionModeId,
                        ORG_BANK_ID = item.OrgBankId,
                        CHEQUE_NO = item.ChequeNo,
                        CHEQUE_DATE = item.CollectionModeId == 1 ? null : ReportHelpers.TryParseDate(item.ChequeDate),
                        USER_ID = SessionHelper.GetLoggedInUserId(HttpContext)
                    }, "USP_SAVE_CLEARING_AND_FORWRDING_PAYMENT");
                }
                var result = new ResponseResult
                {
                    Status = true,
                    Message = "Payments saved successfully."
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

        #endregion CNF Payment
    }
}
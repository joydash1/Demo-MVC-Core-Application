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

        public HomeController(IUnitOfWork unitOfWork, ISpService spService)
        {
            _unitOfWork = unitOfWork;
            _spService = spService;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region Organization
        public async Task<IActionResult> Organization()
        {
            var organizations = await _unitOfWork.OrganizationRepository.GetAllAsync(x => x.IsActive == true);
            var viewModel = new OrganizationViewModel
            {
                OrganizationList = organizations
            };
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> SaveOrganization([FromForm] Organization organization)
        {
            try
            {
               
                if (organization == null)
                {
                    TempData["AlertMessage"] = "Invalid Data.";
                    TempData["AlertType"] = "error";
                    return RedirectToAction("Organization", "Home");
                }
                organization.EntryDate = DateTime.Now;
                organization.EntryUserId = SessionHelper.GetLoggedInUserId(HttpContext) ?? 0;
                organization.IsActive = true;
                await _unitOfWork.OrganizationRepository.AddAsync(organization);
                await _unitOfWork.CommitAsync();
                TempData["AlertMessage"] = "Save successful.";
                TempData["AlertType"] = "success";
                return RedirectToAction("Organization", "Home");
            }
            catch (Exception ex)
            {
                TempData["AlertMessage"] = "An error occurred. Please try again.";
                TempData["AlertType"] = "error";
                return RedirectToAction("Organization", "Home");
            }
        }
        [HttpGet]
        public async Task<IActionResult> DeleteOrganizationById(DeleteorganizationDto deleteorganization)
        {
            try
            {

                if (deleteorganization.Id == 0)
                {
                    TempData["AlertMessage"] = "Invalid Data.";
                    TempData["AlertType"] = "error";
                    return RedirectToAction("Organization", "Home");
                }
                var data = await _spService.GetDataWithParameterAsync<DeleteorganizationDto>(new
                {
                    ID  = deleteorganization.Id,
                    USER_ID  = SessionHelper.GetLoggedInUserId(HttpContext)
                }, "USP_DELETE_ORGANIZATION_BY_ID");
                return Json(new 
                { 
                    Status = true, 
                    Message="Organization Delete Successfully" 
                });
            }
            catch (Exception ex)
            {
                TempData["AlertMessage"] = "An error occurred. Please try again.";
                TempData["AlertType"] = "error";
                return RedirectToAction("Organization", "Home");
            }
        }
        #endregion

        #region Bank
        public async Task<IActionResult> Bank()
        {
            var banks = await _unitOfWork.BankRepository.GetAllAsync(x => x.IsActive == true);
            var viewModel = new BankViewModel
            {
                BankList = banks
            };
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> SaveBank([FromForm] Bank bank)
        {
            try
            {

                if (bank == null)
                {
                    TempData["AlertMessage"] = "Invalid Data.";
                    TempData["AlertType"] = "error";
                    return RedirectToAction("Bank", "Home");
                }
                bank.EntryDate = DateTime.Now;
                bank.EntryUserId = SessionHelper.GetLoggedInUserId(HttpContext) ?? 0;
                bank.IsActive = true;
                await _unitOfWork.BankRepository.AddAsync(bank);
                await _unitOfWork.CommitAsync();
                TempData["AlertMessage"] = "Save successful.";
                TempData["AlertType"] = "success";
                return RedirectToAction("Bank", "Home");
            }
            catch (Exception ex)
            {
                TempData["AlertMessage"] = "An error occurred. Please try again.";
                TempData["AlertType"] = "error";
                return RedirectToAction("Bank", "Home");
            }
        }
        [HttpGet]
        public async Task<IActionResult> DeleteBankById(DeleteBankDto deleteBankDto)
        {
            try
            {

                if (deleteBankDto.Id == 0)
                {
                    TempData["AlertMessage"] = "Invalid Data.";
                    TempData["AlertType"] = "error";
                    return RedirectToAction("Bank", "Home");
                }
                var data = await _spService.GetDataWithParameterAsync<DeleteBankDto>(new
                {
                    ID = deleteBankDto.Id,
                    USER_ID = SessionHelper.GetLoggedInUserId(HttpContext)
                }, "USP_DELETE_BANK_BY_ID");
                return Json(new
                {
                    Status = true,
                    Message = "Bank Delete Successfully"
                });
            }
            catch (Exception ex)
            {
                TempData["AlertMessage"] = "An error occurred. Please try again.";
                TempData["AlertType"] = "error";
                return RedirectToAction("Bank", "Home");
            }
        }
        #endregion

        #region Branch
        public async Task<IActionResult> Branch()
        {
            ViewBag.BankList = await _unitOfWork.BankRepository.GetAllAsync(x => x.IsActive == true);
            ViewBag.BankBranchList = await _spService.GetDataWithoutParameterAsync<BankNBranchDto>("USP_GET_BANK_BRANCH_LIST").ToListAsync();
         
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SaveBranch([FromForm] BankBranch branch)
        {
            try
            {

                if (branch == null)
                {
                    TempData["AlertMessage"] = "Invalid Data.";
                    TempData["AlertType"] = "error";
                    return RedirectToAction("Branch", "Home");
                }
                branch.EntryDate = DateTime.Now;
                branch.EntryUserId = SessionHelper.GetLoggedInUserId(HttpContext) ?? 0;
                branch.IsActive = true;
                await _unitOfWork.BankBranchRepository.AddAsync(branch);
                await _unitOfWork.CommitAsync();
                TempData["AlertMessage"] = "Save successful.";
                TempData["AlertType"] = "success";
                return RedirectToAction("Branch", "Home");
            }
            catch (Exception ex)
            {
                TempData["AlertMessage"] = "An error occurred. Please try again.";
                TempData["AlertType"] = "error";
                return RedirectToAction("Branch", "Home");
            }
        }
        [HttpGet]
        public async Task<IActionResult> DeleteBranchById(DeleteBranchDto delete)
        {
            try
            {

                if (delete.Id == 0)
                {
                    TempData["AlertMessage"] = "Invalid Data.";
                    TempData["AlertType"] = "error";
                    return RedirectToAction("Branch", "Home");
                }
                var data = await _spService.GetDataWithParameterAsync<DeleteBranchDto>(new
                {
                    ID = delete.Id,
                    USER_ID = SessionHelper.GetLoggedInUserId(HttpContext)
                }, "USP_DELETE_BANK_BRANCH_BY_ID");
                return Json(new
                {
                    Status = true,
                    Message = "Branch Delete Successfully"
                });
            }
            catch (Exception ex)
            {
                TempData["AlertMessage"] = "An error occurred. Please try again.";
                TempData["AlertType"] = "error";
                return RedirectToAction("Branch", "Home");
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetBankBranchList()
        {
            try
            {
                var data = await _spService.GetDataWithoutParameterAsync<BankNBranchDto>("USP_GET_BANK_BRANCH_LIST").ToListAsync();
                return Json(new
                {
                    Status = true,
                    DataList = data
                });
            }
            catch (Exception ex)
            {
                TempData["AlertMessage"] = "An error occurred. Please try again.";
                TempData["AlertType"] = "error";
                return RedirectToAction("Branch", "Home");
            }
        }
        #endregion

        #region Organization Account
        public async Task<IActionResult> OrganizationBankBranch()
        {
            ViewBag.OrganizationList = await _unitOfWork.OrganizationRepository.GetAllAsync(x => x.IsActive == true);
            ViewBag.BankList = await _unitOfWork.BankRepository.GetAllAsync(x => x.IsActive == true);
            ViewBag.BranchList =await _unitOfWork.BankBranchRepository.GetAllAsync(x => x.IsActive == true);
            ViewBag.OrganizationBankAccountList = await _spService.GetDataWithoutParameterAsync<OrganizationAccountListDto>("USP_GET_ORGANIZATION_BANK_ACCOUNT_LIST").ToListAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SaveOrganizationBankBranch([FromForm] OrganizationBankAccount account)
        {
            try
            {

                if (account == null)
                {
                    TempData["AlertMessage"] = "Invalid Data.";
                    TempData["AlertType"] = "error";
                    return RedirectToAction("OrganizationBankBranch", "Home");
                }
                account.EntryDate = DateTime.Now;
                account.EntryUserId = SessionHelper.GetLoggedInUserId(HttpContext) ?? 0;
                account.IsActive = true;
                await _unitOfWork.OrganizationBankAccountRepository.AddAsync(account);
                await _unitOfWork.CommitAsync();
                TempData["AlertMessage"] = "Save successful.";
                TempData["AlertType"] = "success";
                return RedirectToAction("OrganizationBankBranch", "Home");
            }
            catch (Exception ex)
            {
                TempData["AlertMessage"] = "An error occurred. Please try again.";
                TempData["AlertType"] = "error";
                return RedirectToAction("OrganizationBankBranch", "Home");
            }
        }
        [HttpGet]
        public async Task<IActionResult> DeleteOrganizationBankBranchById(DeleteOrganizationAccountDto delete)
        {
            try
            {

                if (delete.Id == 0)
                {
                    TempData["AlertMessage"] = "Invalid Data.";
                    TempData["AlertType"] = "error";
                    return RedirectToAction("OrganizationBankBranch", "Home");
                }
                var data = await _spService.GetDataWithParameterAsync<DeleteOrganizationAccountDto>(new
                {
                    ID = delete.Id,
                    USER_ID = SessionHelper.GetLoggedInUserId(HttpContext)
                }, "USP_DELETE_ORGANIZATION_BANK_ACCOUNT_BY_ID");
                return Json(new
                {
                    Status = true,
                    Message = "Organization Bank Accounts Delete Successfully"
                });
            }
            catch (Exception ex)
            {
                TempData["AlertMessage"] = "An error occurred. Please try again.";
                TempData["AlertType"] = "error";
                return RedirectToAction("OrganizationBankBranch", "Home");
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetBankBranchByBankId(DeleteBankDto bank)
        {
            try
            {

                if (bank.Id == 0)
                {
                    TempData["AlertMessage"] = "Invalid Data.";
                    TempData["AlertType"] = "error";
                    return RedirectToAction("OrganizationBankBranch", "Home");
                }
                var data = await _spService.GetDataWithParameterAsync<BranchByBankDto>(new
                {
                    BANK_ID = bank.Id,
                }, "USP_GET_BRANCH_ROUTING_NO_BY_BANK");
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
                return RedirectToAction("OrganizationBankBranch", "Home");
            }
        }
        #endregion
    }
}
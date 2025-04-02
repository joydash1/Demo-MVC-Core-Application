using ERP.DataAccess.Domains;
using ERP.DataAccess.DTOs.BankBranch;
using ERP.DataAccess.DTOs.Basic_Setup;
using ERP.DataAccess.DTOs.Buyer;
using ERP.DataAccess.DTOs.Organization;
using ERP.Infrastructure.Interfaces;
using ERP.Utility.Helpers;
using ERP.Utility.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ERP.WEB.Controllers
{
    public class BasicSetupController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISpService _spService;

        public BasicSetupController(IUnitOfWork unitOfWork, ISpService spService)
        {
            _unitOfWork = unitOfWork;
            _spService = spService;
        }

        #region Organization
        public async Task<IActionResult> Organization()
        {
            var organizations = await _unitOfWork.OrganizationRepository.GetAllAsync(x => x.IsActive == true);
            //var organizations = await _unitOfWork.OrganizationRepository
            //                        .GetAllAsync(
            //                            x => x.IsActive == true,
            //                            (IQueryable<Organization> query) => query.OrderBy(x => x.EntryDate)
            //                        );
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
                    return RedirectToAction("Organization", "BasicSetup");
                }
                organization.EntryDate = DateTime.Now;
                organization.EntryUserId = SessionHelper.GetLoggedInUserId(HttpContext) ?? 0;
                organization.IsActive = true;
                await _unitOfWork.OrganizationRepository.AddAsync(organization);
                await _unitOfWork.CommitAsync();
                TempData["AlertMessage"] = "Save successful.";
                TempData["AlertType"] = "success";
                return RedirectToAction("Organization", "BasicSetup");
            }
            catch (Exception ex)
            {
                TempData["AlertMessage"] = "An error occurred. Please try again.";
                TempData["AlertType"] = "error";
                return RedirectToAction("Organization", "BasicSetup");
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
                    return RedirectToAction("Organization", "BasicSetup");
                }
                var data = await _spService.GetDataWithParameterAsync<DeleteorganizationDto>(new
                {
                    ID = deleteorganization.Id,
                    USER_ID = SessionHelper.GetLoggedInUserId(HttpContext)
                }, "USP_DELETE_ORGANIZATION_BY_ID");
                return Json(new
                {
                    Status = true,
                    Message = "Organization Delete Successfully"
                });
            }
            catch (Exception ex)
            {
                TempData["AlertMessage"] = "An error occurred. Please try again.";
                TempData["AlertType"] = "error";
                return RedirectToAction("Organization", "BasicSetup");
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
                    return RedirectToAction("Bank", "BasicSetup");
                }
                bank.EntryDate = DateTime.Now;
                bank.EntryUserId = SessionHelper.GetLoggedInUserId(HttpContext) ?? 0;
                bank.IsActive = true;
                await _unitOfWork.BankRepository.AddAsync(bank);
                await _unitOfWork.CommitAsync();
                TempData["AlertMessage"] = "Save successful.";
                TempData["AlertType"] = "success";
                return RedirectToAction("Bank", "BasicSetup");
            }
            catch (Exception ex)
            {
                TempData["AlertMessage"] = "An error occurred. Please try again.";
                TempData["AlertType"] = "error";
                return RedirectToAction("Bank", "BasicSetup");
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
                    return RedirectToAction("Bank", "BasicSetup");
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
                return RedirectToAction("Bank", "BasicSetup");
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
                    return RedirectToAction("Branch", "BasicSetup");
                }
                branch.EntryDate = DateTime.Now;
                branch.EntryUserId = SessionHelper.GetLoggedInUserId(HttpContext) ?? 0;
                branch.IsActive = true;
                await _unitOfWork.BankBranchRepository.AddAsync(branch);
                await _unitOfWork.CommitAsync();
                TempData["AlertMessage"] = "Save successful.";
                TempData["AlertType"] = "success";
                return RedirectToAction("Branch", "BasicSetup");
            }
            catch (Exception ex)
            {
                TempData["AlertMessage"] = "An error occurred. Please try again.";
                TempData["AlertType"] = "error";
                return RedirectToAction("Branch", "BasicSetup");
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
                    return RedirectToAction("Branch", "BasicSetup");
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
                return RedirectToAction("Branch", "BasicSetup");
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
                return RedirectToAction("Branch", "BasicSetup");
            }
        }
        #endregion

        #region Organization Account
        public async Task<IActionResult> OrganizationBankBranch()
        {
            ViewBag.OrganizationList = await _unitOfWork.OrganizationRepository.GetAllAsync( x => x.IsActive == true);
            ViewBag.BankList = await _unitOfWork.BankRepository.GetAllAsync(x => x.IsActive == true);
            ViewBag.BranchList = await _unitOfWork.BankBranchRepository.GetAllAsync(x => x.IsActive == true);
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
                    return RedirectToAction("OrganizationBankBranch", "BasicSetup");
                }
                account.EntryDate = DateTime.Now;
                account.EntryUserId = SessionHelper.GetLoggedInUserId(HttpContext) ?? 0;
                account.IsActive = true;
                await _unitOfWork.OrganizationBankAccountRepository.AddAsync(account);
                await _unitOfWork.CommitAsync();
                TempData["AlertMessage"] = "Save successful.";
                TempData["AlertType"] = "success";
                return RedirectToAction("OrganizationBankBranch", "BasicSetup");
            }
            catch (Exception ex)
            {
                TempData["AlertMessage"] = "An error occurred. Please try again.";
                TempData["AlertType"] = "error";
                return RedirectToAction("OrganizationBankBranch", "BasicSetup");
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
                    return RedirectToAction("OrganizationBankBranch", "BasicSetup");
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
                return RedirectToAction("OrganizationBankBranch", "BasicSetup");
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
                    return RedirectToAction("OrganizationBankBranch", "BasicSetup");
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
                return RedirectToAction("OrganizationBankBranch", "BasicSetup");
            }
        }
        #endregion

        #region Product Category
        public async Task<IActionResult> Category()
        {
            ViewBag.ProductCategoryList = await _spService.GetDataWithParameterAsync<CategoryDto>(new { ID = 0 }, "USP_GET_PRODUCT_CATEGORY_LIST").ToListAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> InsertUpdateCategory(CategoryDto category)
        {
            try
            {

                if (category == null)
                {
                    TempData["AlertMessage"] = "Invalid Data.";
                    TempData["AlertType"] = "error";
                    return RedirectToAction("Category", "BasicSetup");
                }
                var data = await _spService.GetDataWithParameterAsync<CategoryDto>(new
                {
                    ID = category.Id,
                    CATEGORY_NAME = category.CategoryName,
                    USER_ID = SessionHelper.GetLoggedInUserId(HttpContext)
                }, "USP_INSERT_UPDATE_PRODUCT_CATEGORY");
                string message = "";

                if (category.Id > 0)
                {
                    message = "Product Category Update Successfully";
                }
                else
                {
                    message = "Product Category Save Successfully";
                }
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
                return RedirectToAction("Category", "BasicSetup");
            }
        }
        [HttpPost]
        public async Task<IActionResult> DeleteCategoryById(CategoryDto delete)
        {
            try
            {

                if (delete.Id == 0)
                {
                    TempData["AlertMessage"] = "Invalid Data.";
                    TempData["AlertType"] = "error";
                    return RedirectToAction("Category", "BasicSetup");
                }
                var data = await _spService.GetDataWithParameterAsync<IndianBuyerDto>(new
                {
                    ID = delete.Id,
                    USER_ID = SessionHelper.GetLoggedInUserId(HttpContext)
                }, "USP_DELETE_PRODUCT_CATEGORY_BY_ID");
                return Json(new
                {
                    Status = true,
                    Message = "Product Category Delete Successfully."
                });
            }
            catch (Exception ex)
            {
                TempData["AlertMessage"] = "An error occurred. Please try again.";
                TempData["AlertType"] = "error";
                return RedirectToAction("Category", "BasicSetup");
            }
        }
        [HttpPost]
        public async Task<IActionResult> GetCategoryById(CategoryDto get)
        {
            try
            {

                if (get.Id == 0)
                {
                    TempData["AlertMessage"] = "Invalid Data.";
                    TempData["AlertType"] = "error";
                    return RedirectToAction("Category", "BasicSetup");
                }
                var data = await _spService.GetDataWithParameterAsync<CategoryDto>(new
                {
                    ID = get.Id
                }, "USP_GET_PRODUCT_CATEGORY_LIST");
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
                return RedirectToAction("Category", "BasicSetup");
            }
        }
        #endregion

        #region Product
        public async Task<IActionResult> Product()
        {
            ViewBag.ProductCategoryList = await _spService.GetDataWithParameterAsync<CategoryDto>(new { ID = 0 }, "USP_GET_PRODUCT_CATEGORY_LIST").ToListAsync();
            ViewBag.ProductList = await _spService.GetDataWithParameterAsync<ProductDto>(new { ID = 0 }, "USP_GET_PRODUCT_LIST").ToListAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> InsertUpdateProduct(ProductDto product)
        {
            try
            {

                if (product == null)
                {
                    TempData["AlertMessage"] = "Invalid Data.";
                    TempData["AlertType"] = "error";
                    return RedirectToAction("Product", "BasicSetup");
                }
                var data = await _spService.GetDataWithParameterAsync<CategoryDto>(new
                {
                    ID = product.Id,
                    CATEGORY_ID = product.CategoryId,
                    PRODUCT_NAME = product.ProductName,
                    USER_ID = SessionHelper.GetLoggedInUserId(HttpContext)
                }, "USP_INSERT_UPDATE_PRODUCT");
                string message = "";

                if (product.Id > 0)
                {
                    message = "Product Update Successfully";
                }
                else
                {
                    message = "Product Save Successfully";
                }
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
                return RedirectToAction("Product", "BasicSetup");
            }
        }
        [HttpPost]
        public async Task<IActionResult> DeleteProductById(ProductDto delete)
        {
            try
            {

                if (delete.Id == 0)
                {
                    TempData["AlertMessage"] = "Invalid Data.";
                    TempData["AlertType"] = "error";
                    return RedirectToAction("Product", "BasicSetup");
                }
                var data = await _spService.GetDataWithParameterAsync<IndianBuyerDto>(new
                {
                    ID = delete.Id,
                    USER_ID = SessionHelper.GetLoggedInUserId(HttpContext)
                }, "USP_DELETE_PRODUCT_BY_ID");
                return Json(new
                {
                    Status = true,
                    Message = "Product Delete Successfully."
                });
            }
            catch (Exception ex)
            {
                TempData["AlertMessage"] = "An error occurred. Please try again.";
                TempData["AlertType"] = "error";
                return RedirectToAction("Product", "BasicSetup");
            }
        }
        [HttpPost]
        public async Task<IActionResult> GetProductById(ProductDto get)
        {
            try
            {

                if (get.Id == 0)
                {
                    TempData["AlertMessage"] = "Invalid Data.";
                    TempData["AlertType"] = "error";
                    return RedirectToAction("Product", "BasicSetup");
                }
                var data = await _spService.GetDataWithParameterAsync<ProductDto>(new
                {
                    ID = get.Id
                }, "USP_GET_PRODUCT_LIST");
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
                return RedirectToAction("Product", "BasicSetup");
            }
        }
        #endregion

        #region Insurance
        public async Task<IActionResult> Insurance()
        {
            ViewBag.InsuranceList = await _spService.GetDataWithParameterAsync<InsuranceDto>(
                new { ID = 0 }, "USP_GET_INSURANCE_LIST").ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> InsertUpdateInsurance(InsuranceDto insurance)
        {
            try
            {
                if (insurance == null)
                {
                    TempData["AlertMessage"] = "Invalid Data.";
                    TempData["AlertType"] = "error";
                    return RedirectToAction("Insurance", "BasicSetup");
                }

                var data = await _spService.GetDataWithParameterAsync<InsuranceDto>(new
                {
                    ID = insurance.Id,
                    INSURANCE_NAME = insurance.InsuranceName,
                    USER_ID = SessionHelper.GetLoggedInUserId(HttpContext)
                }, "USP_INSERT_UPDATE_INSURANCE");

                string message = insurance.Id > 0 ? "Insurance Update Successfully" : "Insurance Save Successfully";

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
                return RedirectToAction("Insurance", "BasicSetup");
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteInsuranceById(InsuranceDto delete)
        {
            try
            {
                if (delete.Id == 0)
                {
                    TempData["AlertMessage"] = "Invalid Data.";
                    TempData["AlertType"] = "error";
                    return RedirectToAction("Insurance", "BasicSetup");
                }

                var data = await _spService.GetDataWithParameterAsync<InsuranceDto>(new
                {
                    ID = delete.Id,
                    USER_ID = SessionHelper.GetLoggedInUserId(HttpContext)
                }, "USP_DELETE_INSURANCE_BY_ID");

                return Json(new
                {
                    Status = true,
                    Message = "Insurance Deleted Successfully."
                });
            }
            catch (Exception ex)
            {
                TempData["AlertMessage"] = "An error occurred. Please try again.";
                TempData["AlertType"] = "error";
                return RedirectToAction("Insurance", "BasicSetup");
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetInsuranceById(InsuranceDto get)
        {
            try
            {
                if (get.Id == 0)
                {
                    TempData["AlertMessage"] = "Invalid Data.";
                    TempData["AlertType"] = "error";
                    return RedirectToAction("Insurance", "BasicSetup");
                }

                var data = await _spService.GetDataWithParameterAsync<InsuranceDto>(new
                {
                    ID = get.Id
                }, "USP_GET_INSURANCE_LIST");

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
                return RedirectToAction("Insurance", "BasicSetup");
            }
        }

        #endregion

        #region Border
        public async Task<IActionResult> Border()
        {
            ViewBag.BorderList = await _spService.GetDataWithParameterAsync<BorderDto>(new { ID = 0 }, "USP_GET_BORDER_LIST").ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> InsertUpdateBorder(BorderDto border)
        {
            try
            {
                if (border == null)
                {
                    TempData["AlertMessage"] = "Invalid Data.";
                    TempData["AlertType"] = "error";
                    return RedirectToAction("Border", "BasicSetup");
                }

                var data = await _spService.GetDataWithParameterAsync<BorderDto>(new
                {
                    ID = border.Id,
                    BORDER_NAME = border.BorderName,
                    USER_ID = SessionHelper.GetLoggedInUserId(HttpContext)
                }, "USP_INSERT_UPDATE_BORDER");

                string message = border.Id > 0 ? "Border Updated Successfully" : "Border Saved Successfully";

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
                return RedirectToAction("Border", "BasicSetup");
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBorderById(BorderDto delete)
        {
            try
            {
                if (delete.Id == 0)
                {
                    TempData["AlertMessage"] = "Invalid Data.";
                    TempData["AlertType"] = "error";
                    return RedirectToAction("Border", "BasicSetup");
                }

                var data = await _spService.GetDataWithParameterAsync<BorderDto>(new
                {
                    ID = delete.Id,
                    USER_ID = SessionHelper.GetLoggedInUserId(HttpContext)
                }, "USP_DELETE_BORDER_BY_ID");

                return Json(new
                {
                    Status = true,
                    Message = "Border Deleted Successfully."
                });
            }
            catch (Exception ex)
            {
                TempData["AlertMessage"] = "An error occurred. Please try again.";
                TempData["AlertType"] = "error";
                return RedirectToAction("Border", "BasicSetup");
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetBorderById(BorderDto get)
        {
            try
            {
                if (get.Id == 0)
                {
                    TempData["AlertMessage"] = "Invalid Data.";
                    TempData["AlertType"] = "error";
                    return RedirectToAction("Border", "BasicSetup");
                }

                var data = await _spService.GetDataWithParameterAsync<BorderDto>(new
                {
                    ID = get.Id
                }, "USP_GET_BORDER_LIST");

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
                return RedirectToAction("Border", "BasicSetup");
            }
        }
        #endregion
    }
}

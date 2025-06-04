using ERP.DataAccess.Domains;
using ERP.DataAccess.DTOs.Buyer;
using ERP.DataAccess.DTOs.ClearingAndFordwingCNF;
using ERP.DataAccess.DTOs.Seller;
using ERP.Infrastructure.Interfaces;
using ERP.Utility.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ERP.WEB.Controllers
{
    public class BuyerSellerController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISpService _spService;

        public BuyerSellerController(IUnitOfWork unitOfWork, ISpService spService)
        {
            _unitOfWork = unitOfWork;
            _spService = spService;
        }

        #region Indian Buyer

        public async Task<IActionResult> IndianBuyer()
        {
            ViewBag.IndianBuyerList = await _spService.GetDataWithParameterAsync<IndianBuyerDto>(new { ID = 0 }, "USP_GET_INDIAN_BUYER_LIST").ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> InsertUpdateIndianBuyer(IndianBuyerDto buyer)
        {
            try
            {
                if (buyer == null)
                {
                    TempData["AlertMessage"] = "Invalid Data.";
                    TempData["AlertType"] = "error";
                    return RedirectToAction("IndianBuyer", "BuyerSeller");
                }
                var data = await _spService.GetDataWithParameterAsync<IndianBuyerDto>(new
                {
                    ID = buyer.Id,
                    BUYER_NAME = buyer.BuyerName,
                    SHOP_NAME = buyer.ShopName,
                    SHOP_ADDRESS = buyer.Address,
                    SHOP_WEBSITE = buyer.Website,
                    SHOP_TINNO = buyer.TINNumber,
                    USER_ID = SessionHelper.GetLoggedInUserId(HttpContext)
                }, "USP_INSERT_UPDATE_INDIAN_BUTYER");
                string message = "";

                if (buyer.Id > 0)
                {
                    message = "Indian Buyer Update Successfully";
                }
                else
                {
                    message = "Indian Buyer Save Successfully";
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
                return RedirectToAction("IndianBuyer", "BuyerSeller");
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteIndianBuyerById(IndianBuyerDto delete)
        {
            try
            {
                if (delete.Id == 0)
                {
                    TempData["AlertMessage"] = "Invalid Data.";
                    TempData["AlertType"] = "error";
                    return RedirectToAction("IndianBuyer", "BuyerSeller");
                }
                var data = await _spService.GetDataWithParameterAsync<IndianBuyerDto>(new
                {
                    ID = delete.Id,
                    USER_ID = SessionHelper.GetLoggedInUserId(HttpContext)
                }, "USP_DELETE_INDIAN_BUYER_BY_ID");
                return Json(new
                {
                    Status = true,
                    Message = "Indian Buyer Delete Successfully."
                });
            }
            catch (Exception ex)
            {
                TempData["AlertMessage"] = "An error occurred. Please try again.";
                TempData["AlertType"] = "error";
                return RedirectToAction("IndianBuyer", "BuyerSeller");
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetIndianBuyerById(IndianBuyerDto get)
        {
            try
            {
                if (get.Id == 0)
                {
                    TempData["AlertMessage"] = "Invalid Data.";
                    TempData["AlertType"] = "error";
                    return RedirectToAction("IndianBuyer", "BuyerSeller");
                }
                var data = await _spService.GetDataWithParameterAsync<IndianBuyerDto>(new
                {
                    ID = get.Id
                }, "USP_GET_INDIAN_BUYER_LIST");
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
                return RedirectToAction("IndianBuyer", "BuyerSeller");
            }
        }

        #endregion Indian Buyer

        #region Bangladeshi Seller

        public async Task<IActionResult> BangladeshiSeller()
        {
            ViewBag.BangladeshiSellerList = await _spService.GetDataWithParameterAsync<BangladeshiSellerDto>(new { ID = 0 }, "USP_GET_BANGLADESHI_SELLER_LIST").ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> BangladeshiSellerList(BangladeshiSellerDto get)
        {
            try
            {
                var data = await _spService.GetDataWithParameterAsync<BangladeshiSellerDto>(new { ID = 0 }, "USP_GET_BANGLADESHI_SELLER_LIST").ToListAsync();

                return Json(new ResponseListResult<List<BangladeshiSellerDto>>
                {
                    Status = true,
                    Data = (List<BangladeshiSellerDto>)data
                });
            }
            catch (Exception ex)
            {
                TempData["AlertMessage"] = "An error occurred. Please try again.";
                TempData["AlertType"] = "error";
                return RedirectToAction("BangladeshiSeller", "BuyerSeller");
            }
        }

        [HttpPost]
        public async Task<IActionResult> InsertUpdateBangladeshiSeller(BangladeshiSellerDto seller)
        {
            try
            {
                if (seller == null)
                {
                    TempData["AlertMessage"] = "Invalid Data.";
                    TempData["AlertType"] = "error";
                    return RedirectToAction("BangladeshiSeller", "BuyerSeller");
                }
                var data = await _spService.GetDataWithParameterAsync<BangladeshiSellerDto>(new
                {
                    ID = seller.Id,
                    SELLER_NAME = seller.SellerName,
                    SHOP_NAME = seller.ShopName,
                    SHOP_ADDRESS = seller.Address,
                    SHOP_WEBSITE = seller.Website,
                    SHOP_TINNO = seller.TINNumber,
                    MOBILE_NO = seller.MobileNo,
                    EMAIL_ADDRESS = seller.EmailAddress,
                    USER_ID = SessionHelper.GetLoggedInUserId(HttpContext)
                }, "USP_INSERT_UPDATE_BANGLADESHI_SELLER");
                string message = "";

                if (seller.Id > 0)
                {
                    message = "Bangladeshi Seller Update Successfully";
                }
                else
                {
                    message = "Bangladeshi Seller Save Successfully";
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
                return RedirectToAction("BangladeshiSeller", "BuyerSeller");
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBangladeshiSellerById(BangladeshiSellerDto delete)
        {
            try
            {
                if (delete.Id == 0)
                {
                    TempData["AlertMessage"] = "Invalid Data.";
                    TempData["AlertType"] = "error";
                    return RedirectToAction("BangladeshiSeller", "BuyerSeller");
                }
                var data = await _spService.GetDataWithParameterAsync<BangladeshiSellerDto>(new
                {
                    ID = delete.Id,
                    USER_ID = SessionHelper.GetLoggedInUserId(HttpContext)
                }, "USP_DELETE_BANGLADESHI_SELLER_BY_ID");
                return Json(new
                {
                    Status = true,
                    Message = "Bangladeshi Seller Delete Successfully."
                });
            }
            catch (Exception ex)
            {
                TempData["AlertMessage"] = "An error occurred. Please try again.";
                TempData["AlertType"] = "error";
                return RedirectToAction("BangladeshiSeller", "BuyerSeller");
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetBangladeshiSellerById(BangladeshiSellerDto get)
        {
            try
            {
                if (get.Id == 0)
                {
                    TempData["AlertMessage"] = "Invalid Data.";
                    TempData["AlertType"] = "error";
                    return RedirectToAction("BangladeshiSeller", "BuyerSeller");
                }
                var data = await _spService.GetDataWithParameterAsync<BangladeshiSellerDto>(new
                {
                    ID = get.Id
                }, "USP_GET_BANGLADESHI_SELLER_LIST");
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
                return RedirectToAction("BangladeshiSeller", "BuyerSeller");
            }
        }

        #endregion Bangladeshi Seller
    }
}
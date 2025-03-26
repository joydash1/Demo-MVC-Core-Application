using ERP.DataAccess.Domains;
using ERP.Infrastructure.Interfaces;
using ERP.Utility.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;

namespace ERP.WEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region Organization
        public IActionResult Organization()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SaveOrganizationAsync(Organization organization)
        {
            try
            {
               
                if (organization == null)
                {
                    TempData["AlertMessage"] = "Invalid Data.";
                    TempData["AlertType"] = "error";
                    return RedirectToAction("Organization", "Home");
                }
                organization.EntryDate = DateTime.UtcNow;
                organization.EntryUserId = 1;
                organization.IsActive = true;
                await _unitOfWork.OrganizationRepository.AddAsync(organization);
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
        #endregion
    }
}

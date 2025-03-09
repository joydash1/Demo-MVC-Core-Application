using ERP.DataAccess.Domains;
using ERP.Infrastructure.Interfaces;
using ERP.Utility.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ERP.WEB.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AuthenticationController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        #region Registration
        public ActionResult Registration()
        {
            return View();
        }
        public async Task<IActionResult> UserRegistration([FromForm] ApplicationUser model)
        {

            try
            {
                if (string.IsNullOrWhiteSpace(model.FirstName))
                {
                    TempData["AlertMessage"] = "First Name is required.";
                    TempData["AlertType"] = "error";
                }
                if (string.IsNullOrWhiteSpace(model.LastName))
                {
                    TempData["AlertMessage"] = "Last Name is required.";
                    TempData["AlertType"] = "error";
                }
                if (string.IsNullOrWhiteSpace(model.EmployeeCode))
                {
                    TempData["AlertMessage"] = "EmployeeCode is required.";
                    TempData["AlertType"] = "error";
                }
                if (string.IsNullOrWhiteSpace(model.MobileNo))
                {
                    TempData["AlertMessage"] = "Mobile No is required.";
                    TempData["AlertType"] = "error";
                }
                if (string.IsNullOrWhiteSpace(model.Password))
                {
                    TempData["AlertMessage"] = "Password  is required.";
                    TempData["AlertType"] = "error";
                }
                if (model.Image != null)
                {
                    var fileDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads", "ProfilePictures");

                    if (!Directory.Exists(fileDirectory))
                    {
                        Directory.CreateDirectory(fileDirectory);
                    }
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                    string filePath = Path.Combine(fileDirectory, uniqueFileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.Image.CopyToAsync(stream);
                    }

                    model.ProfilePicture = uniqueFileName;
                    model.ProfilePicturePath = Path.Combine("Uploads", "ProfilePictures", uniqueFileName);
                }
                var passwordHash = PasswordHashHelpers.HashPasswordWithSalt(model.Password);
                model.Password = passwordHash;
                await _unitOfWork.ApplicationUser.AddAsync(model);
                await _unitOfWork.CommitAsync();
                TempData["AlertMessage"] = "User Registration successfully";
                TempData["AlertType"] = "success";
                return RedirectToAction("Index","Home");
            }
            catch (Exception ex)
            {
                TempData["AlertMessage"] = $"{ ex.Message}";
                TempData["AlertType"] = "error";
                return RedirectToAction("Registration", "Authentication");
            }
        }
        #endregion

        #region Login
        public ActionResult Login()
        {
            return View();
        }
        #endregion
    }
}

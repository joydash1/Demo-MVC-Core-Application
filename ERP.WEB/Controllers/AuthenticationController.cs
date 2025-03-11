using ERP.DataAccess.Domains;
using ERP.Infrastructure.Interfaces;
using ERP.Utility.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

namespace ERP.WEB.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;
        private readonly ISpService _spService;

        public AuthenticationController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment, IConfiguration configuration, ISpService spService)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            _spService = spService;
        }
        #region Registration
        public ActionResult Registration()
        {
            return View();
        }
        [HttpPost]
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
        public async Task<ActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UserLogin([FromForm] ApplicationUser model)
        {
            try
            {
                var user = await _unitOfWork.ApplicationUser.GetAsync(u => u.Email.Trim() == model.Email.Trim());

                if (user == null || !PasswordHashHelpers.VerifyPasswordWithSalt(model.Password, user.Password))
                {
                    TempData["AlertMessage"] = "Invalid email or password.";
                    TempData["AlertType"] = "error";
                    return RedirectToAction("Login", "Authentication");
                }

                var jwtHelper = new JwtTokenHelper(_configuration);
                user.IsLoggedIn = true;
                user.JwtToken = jwtHelper.GenerateJwtToken(user);
                user.RefreshToken = jwtHelper.GenerateRefreshToken();
                user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

                await _unitOfWork.CommitAsync();
               
                Response.Cookies.Append("jwtToken", user.JwtToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.UtcNow.AddMinutes(30)
                });

                TempData["AlertMessage"] = "Login successful.";
                TempData["AlertType"] = "success";
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                TempData["AlertMessage"] = "An error occurred. Please try again.";
                TempData["AlertType"] = "error";

                return RedirectToAction("Login", "Authentication");
            }
        }
        #endregion

        #region Refresh Token
        public async Task<IActionResult> RefreshToken(string refreshToken)
        {
            var user = await _unitOfWork.ApplicationUser.GetAsync(u => u.RefreshToken == refreshToken);

            if (user == null || user.RefreshTokenExpiryTime < DateTime.UtcNow)
            {
                return Unauthorized(new { message = "Invalid or expired refresh token." });
            }

            var jwtHelper = new JwtTokenHelper(_configuration);

            user.JwtToken = jwtHelper.GenerateJwtToken(user);
            user.RefreshToken = jwtHelper.GenerateRefreshToken();
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            await _unitOfWork.CommitAsync();

            return Ok(new { token = user.JwtToken, refreshToken = user.RefreshToken });
        }

        #endregion
    }
}

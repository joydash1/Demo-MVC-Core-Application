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

        public ActionResult Registration()
        {
            return View();
        }
        public async Task<IActionResult> UserRegistration(ApplicationUser model, IFormFile? file)
        {

            try
            {
                if (file != null)
                {
                    var fileDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads", "ProfilePictures");

                    if (!Directory.Exists(fileDirectory))
                    {
                        Directory.CreateDirectory(fileDirectory);
                    }
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                    string filePath = Path.Combine(fileDirectory, uniqueFileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    model.ProfilePicture = uniqueFileName;
                    model.ProfilePicturePath = Path.Combine("Uploads", "ProfilePictures", uniqueFileName);
                }

                var passwordHash = PasswordHashHelpers.HashPasswordWithSalt(model.Password);
                model.Password = passwordHash;
                await _unitOfWork.ApplicationUser.AddAsync(model);
                await _unitOfWork.CommitAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}

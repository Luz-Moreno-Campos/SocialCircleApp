using Microsoft.AspNetCore.Mvc;
using SocialCircle.BLL;
using SocialCircle.Models.ViewModels;

namespace SocialCircle.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserService _userService;

        public AccountController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            bool success = _userService.RegisterUser(
                model.UserName,
                model.Email,
                model.Password
            );

            if (!success)
            {
                ViewBag.Error = "Email already exists.";
                return View(model);
            }

            TempData["SuccessMessage"] = "Account created successfully!";
            return RedirectToAction("Login", "User");
        }
    }
}

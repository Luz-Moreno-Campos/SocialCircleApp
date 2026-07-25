using Microsoft.AspNetCore.Mvc;
using SocialCircle.BLL;
using SocialCircle.Models.ViewModels;

namespace SocialCircle.MVC.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService _userService;
        private readonly FollowService _followService;

        public UserController(UserService userService, FollowService followService)
        {
            _userService = userService;
            _followService = followService;
        }


        
        public IActionResult Login()
        {
            return View();
        }

        
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            var user = _userService.ValidateLogin(model.UserName, model.Password);

            if (user == null)
            {
                ViewBag.Error = "Invalid username or password";
                return View(model);
            }

            HttpContext.Session.SetInt32("CurrentUserId", (int)user.UserId);

            return RedirectToAction("Profile", new { id = user.UserId });
        }

        public IActionResult Profile(long id)
        {
            var vm = _userService.GetProfile(id);

            if (vm == null)
                
                return NotFound();

            return View(vm);
        }

      
    }
}

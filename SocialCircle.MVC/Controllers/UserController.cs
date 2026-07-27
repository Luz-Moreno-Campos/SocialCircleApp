using Microsoft.AspNetCore.Mvc;
using SocialCircle.BLL;
using SocialCircle.Models.ViewModels;

namespace SocialCircle.MVC.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService _userService;
        private readonly PostService _postService; 

        public UserController(UserService userService, PostService postService) 
        {
            _userService = userService;
            _postService = postService; 
        }

        [HttpGet]
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

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Profile(long id)
        {
            var currentUserId = HttpContext.Session.GetInt32("CurrentUserId");
            
            var vm = _userService.GetProfile(id, currentUserId.Value);

            vm.Posts = _postService.GetPostsByUser(id);
            
            return View("Profile", vm);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using SocialCircle.BLL;
using SocialCircle.Models.ViewModels;

namespace SocialCircle.MVC.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService _userService;
        private readonly PostService _postService; 
        private readonly StoryService _storyService;

        public UserController(UserService userService, PostService postService, StoryService storyService) 
        {
            _userService = userService;
            _postService = postService; 
            // Ferguson added
            _storyService = storyService;
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

            if (currentUserId == null)
                return RedirectToAction("Login", "User");

            var userProfile = _userService.GetProfile(id, currentUserId.Value);

            if (userProfile == null)
                return NotFound();

            userProfile.Posts = _postService.GetPostsByUser(id);

            // Ferguson added
            userProfile.Stories = _storyService.GetStoriesByUser((int)id);

            return View("Profile", userProfile);
        }

    }
}

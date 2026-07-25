using Microsoft.AspNetCore.Mvc;
using SocialCircle.BLL;

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
        public IActionResult Index()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using SocialCircle.BLL;
using SocialCircle.Models.ViewModels;

namespace SocialCircle.MVC.Controllers
{
    public class FollowController : Controller
    {
        private readonly UserService _userService;
        private readonly FollowService _followService;

        public FollowController(UserService userService, FollowService followService)
        {
            _userService = userService;
            _followService = followService;
        }
        [HttpGet]
        public IActionResult Followers(long id)
        {
            var user = _userService.GetUserById(id);
            var followers = _followService.GetFollowers(id);

            var vm = new FollowersViewModel
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Initials = user.UserName.Substring(0, 1).ToUpper(),
                Followers = followers
            };

            ViewBag.OwnerName = user.UserName;   

            return View(followers);              
        }

        [HttpGet]
        public IActionResult Following(long id)
        {
            var user = _userService.GetUserById(id);
            var following = _followService.GetFollowing(id);

            var vm = new FollowingViewModel
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Initials = user.UserName.Substring(0, 1).ToUpper(),
                Following = following
            };

            ViewBag.OwnerName = user.UserName;  

            return View(following);              
        }


        [HttpPost]
        public IActionResult Follow(long targetId)
        {
            var currentUserId = HttpContext.Session.GetInt32("CurrentUserId");

            _followService.Follow(currentUserId.Value, targetId);

            return Redirect("/User/Profile/" + targetId);
        }

        [HttpPost]
        public IActionResult Unfollow(long targetId)
        {
            var currentUserId = HttpContext.Session.GetInt32("CurrentUserId");

            _followService.Unfollow(currentUserId.Value, targetId);

            return Redirect("/User/Profile/" + targetId);
        }
    }
}


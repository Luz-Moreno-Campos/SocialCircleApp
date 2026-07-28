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

        public IActionResult Followers(long id)
        {
            var user = _userService.GetUserById(id);

            if (user == null)
                return NotFound();

            var followers = _followService.GetFollowers(id);

            var follower = new FollowersViewModel
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

            if (user == null)
                return NotFound();

            var following = _followService.GetFollowing(id);

            var newFollowing = new FollowingViewModel
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

            if (currentUserId == null)
                return RedirectToAction("Login", "User");

            _followService.Follow(currentUserId.Value, targetId);

            return Redirect("/User/Profile/" + targetId);
        }


        [HttpPost]
        public IActionResult Unfollow(long targetId)
        {
            var currentUserId = HttpContext.Session.GetInt32("CurrentUserId");

            if (currentUserId == null)
                return RedirectToAction("Login", "User");

            _followService.Unfollow(currentUserId.Value, targetId);

            return Redirect("/User/Profile/" + targetId);
        }
    }
}


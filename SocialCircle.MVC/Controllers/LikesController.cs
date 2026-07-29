using Microsoft.AspNetCore.Mvc;
using SocialCircle.MVC.Models;

namespace SocialCircle.MVC.Controllers
{
    public class LikesController : Controller
    {
        private static List<Like> likes = new List<Like>
        {
            new Like { LikeId = 1, UserId = 1, PostId = 1 },
            new Like { LikeId = 2, UserId = 2, PostId = 1 }
        };

        public IActionResult Index()
        {
            return View(likes);
        }

        [HttpPost]
        public IActionResult Create(int postId)
        {
            var currentUserId = HttpContext.Session.GetInt32("UserId");

            if (currentUserId == null)
            {
                return RedirectToAction("Login", "User");
            }

            bool alreadyLiked = likes.Any(l =>
                l.PostId == postId &&
                l.UserId == currentUserId.Value);

            if (!alreadyLiked)
            {
                likes.Add(new Like
                {
                    LikeId = likes.Count + 1,
                    UserId = currentUserId.Value,
                    PostId = postId
                });
            }

            return RedirectToAction("Index", "Home");
        }

        // Método estático para usar en la vista
        public static int GetLikeCount(long postId)
        {
            return likes.Count(l => l.PostId == postId);
        }
    }
}

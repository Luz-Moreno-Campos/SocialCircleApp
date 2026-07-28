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

        // Returns the number of likes for one post
        public static int GetLikeCount(int postId)
        {
            return likes.Count(l => l.PostId == postId);
        }

        internal static object? GetLikeCount(long postId)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public IActionResult Create(int postId)
        {
            var currentUserId = HttpContext.Session.GetInt32("CurrentUserId");

            if (currentUserId == null)
            {
                return RedirectToAction("Login", "User");
            }

            // Prevent the same user from liking the same post twice
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
    }
}
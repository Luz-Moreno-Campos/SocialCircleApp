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


        public static int GetLikeCount(int postId)
        {
            return likes.Count(l => l.PostId == postId);
        }

    
        [HttpPost]
        public IActionResult Create(int postId)
        {
            var currentUserId = HttpContext.Session.GetInt32("CurrentUserId");

          
            if (currentUserId == null)
            {
                return RedirectToAction("Login", "User");
            }


            var like = new Like
            {
                LikeId = likes.Count + 1,
                UserId = currentUserId.Value,
                PostId = postId
            };


            likes.Add(like);


    
            return RedirectToAction("Index", "Home");
        }
    }
}
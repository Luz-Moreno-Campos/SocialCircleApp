using Microsoft.AspNetCore.Mvc;
using SocialCircle.MVC.Models;

namespace SocialCircle.MVC.Controllers
{
    public class LikesController1 : Controller
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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Like like)
        {
            like.LikeId = likes.Count + 1;
            likes.Add(like);

            return RedirectToAction("Index");
        } 
    }
}

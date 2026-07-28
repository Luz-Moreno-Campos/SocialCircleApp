using Microsoft.AspNetCore.Mvc;
using SocialCircle.BLL;
using SocialCircle.Models;
using SocialCircle.MVC.Models;
using System.Diagnostics;

namespace SocialCircle.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PostService _postService;

        public HomeController(ILogger<HomeController> logger, PostService postService)
        {
            _logger = logger;
            _postService = postService;
        }

        public async Task<IActionResult> Index()
        {
            var currentUserId = HttpContext.Session.GetInt32("CurrentUserId");

            if (currentUserId == null)
            {
                return View();
            }


            var posts = await _postService.GetAllPostsAsync();
            return View(posts);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

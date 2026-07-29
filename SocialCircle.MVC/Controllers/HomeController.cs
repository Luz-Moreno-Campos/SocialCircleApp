using Microsoft.AspNetCore.Mvc;
using SocialCircle.BLL;
using SocialCircle.Models;
using SocialCircle.Models.ViewModels;
using SocialCircle.MVC.Models;
using System.Diagnostics;

namespace SocialCircle.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PostService _postService;
        private readonly StoryService _storyService;

        public HomeController(ILogger<HomeController> logger, PostService postService, StoryService storyService)
        {
            _logger = logger;
            _postService = postService;
            _storyService = storyService;
        }

        public async Task<IActionResult> Index()
        {
            var currentUserId = HttpContext.Session.GetInt32("CurrentUserId");

            if (currentUserId == null)
            {
                return View();
            }
            var homeView = new HomeViewModel
            {
                Posts = await _postService.GetAllPostsAsync(),
                Stories = _storyService.GetAllStories(),
            };
            
            // var posts = await _postService.GetAllPostsAsync();
            return View(homeView);
        }

        [HttpPost]
        public IActionResult Create(Story story)
        {
            var userId = HttpContext.Session.GetInt32("CurrentUserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "User");
            }

            story.UserId = userId.Value;
            story.CreationTimestamp = DateTime.Now;
            story.ExpirationDate = DateTime.Now.AddHours(24);

            _storyService.CreateStory(story);

            return RedirectToAction("Index", "Home");
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

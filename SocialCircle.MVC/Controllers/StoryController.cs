using Microsoft.AspNetCore.Mvc;
using SocialCircle.BLL;
using SocialCircle.Models;
using SocialCircle.Models.ViewModels;


namespace SocialCircle.MVC.Controllers
{
    public class StoryController : Controller
    {
        private StoryService _storyService;

        public StoryController (StoryService storyService)
        {
            _storyService = storyService;
        }

        public IActionResult Index(int id)
        {
            var story = _storyService.GetStory(id);
            if (story == null)
            {
                return NotFound();
            }
            return View(story);
        }

        [HttpPost]
        public IActionResult Create([Bind("StoryText")] Story story)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Profile", "User", new 
                { 
                    id = HttpContext.Session.GetInt32("CurrentUserId") 
                });
            }

            var userId = HttpContext.Session.GetInt32("CurrentUserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "User");
            }

            story.UserId = userId.Value;
            story.CreationTimestamp = DateTime.Now;
            story.ExpirationDate = DateTime.Now.AddHours(24);

            _storyService.Create(story);

            return RedirectToAction("Profile", "User", new { id = userId.Value });
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _storyService.Delete(id);

            return RedirectToAction("Index", "Home");
        }

        // [HttpGet]
        // public IActionResult Edit (int id)
        // {
        //     var story = _storyService.GetStory(id);

        //     if(story == null)
        //     {
        //         return NotFound();
        //     }

        //     return View(story);
        // }

        // [HttpPost]
        // public IActionResult Edit (Post updatedPost)
        // {
        //     if (!ModelState.IsValid)
        //     {
                
        //     }
        // }
    }
}
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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Create (Story story)
        {
            if (!ModelState.IsValid)
            {
                return View(story);
            }
            story.CreationTimestamp = DateTime.Now;
            story.ExpirationDate = DateTime.Now.AddHours(24);
            _storyService.Create(story);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit (int id)
        {
            if (!ModelState.IsValid)
            {
                return null;
            }
            var story = _storyService.GetStory(id);
            return View(story);
        }

        // [HttpPost]
        // public IActionResult Edit (Post updatedPost)
        // {
        //     if (!ModelState.IsValid)
        //     {
                
        //     }
        // }
    }
}
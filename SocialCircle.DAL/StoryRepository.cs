using SocialCircle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialCircle.DAL
{
    public class StoryRepository
    {
        private readonly SocialCircleContext _context;

        public StoryRepository(SocialCircleContext context)
        {
            _context = context;
        }
        
        public void Create (Story story)
        {
            _context.Stories.Add(story);
            _context.SaveChanges();
        }
        public Story GetStory(int id)
        {
            var thisStory = _context.Stories.FirstOrDefault(x => x.User.UserId == id);
            return thisStory;
        }

        public List<Story> GetAllStories()
        {
            var allStories = _context.Stories.ToList();
            return allStories;
        }

        public void updateStory(Story updatedStory)
        {
            var story = _context.Stories.FirstOrDefault(s => s.User.UserId == updatedStory.User.UserId);

            if(story == null) return;
            
            story.StoryText = updatedStory.StoryText;
            story.CreationTimestamp = DateTime.Now;

            _context.SaveChanges();
        }

        public void deleteStory (int id)
        {
            var story = _context.Stories.FirstOrDefault(s => s.User.UserId == id);

            if (story == null) return;

            _context.Stories.Remove(story);
            _context.SaveChanges();
        }
    }
}

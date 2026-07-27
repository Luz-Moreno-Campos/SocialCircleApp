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
        
        public List<Story> GetStory(int id)
        {
            var thisStory = _context.Stories.Where(x => x.UserId == id).ToList();
            if (!thisStory.Any())
            {
                return null;
            }
            return thisStory;
        }

        public List<Story> GetAllStories()
        {
            var allStories = _context.Stories.ToList();
            return allStories;
        }
    }
}

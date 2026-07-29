using SocialCircle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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
            Console.WriteLine("Before SaveChanges");

            _context.Stories.Add(story);
            _context.SaveChanges();

            Console.WriteLine("After SaveChanges");
        }

        public Story? GetActiveStory(int userId)
        {
            return _context.Stories
                .FirstOrDefault(s =>
                    s.UserId == userId &&
                    s.ExpirationDate > DateTime.Now);
        }
        // public Story? GetStory(int id)
        // {
        //     var thisStory = _context.Stories.FirstOrDefault(x => x.User.UserId == id);
        //     if (thisStory == null)
        //     {
        //         return null;
        //     }
        //     return thisStory;
        // }

        public Story? GetStory(int id)
        {
            return _context.Stories
                .FirstOrDefault(s => s.StoryId == id);
        }

        public List<Story> GetAllStories()
        {
            return _context.Stories
            .Where(s => s.ExpirationDate > DateTime.Now)
            .ToList();
        }
        public List<Story> GetStoriesByUser(int userId)
        {
            return _context.Stories
                .Where(s => s.UserId == userId &&
                            s.ExpirationDate > DateTime.Now)
                .OrderByDescending(s => s.CreationTimestamp)
                .ToList();
        }
        public void deleteStory (int id)
        {
            var story = _context.Stories.FirstOrDefault(s => s.StoryId == id);

            if (story == null) return;

            _context.Stories.Remove(story);
            _context.SaveChanges();
        }
    }
}

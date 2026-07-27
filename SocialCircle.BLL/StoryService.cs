using SocialCircle.DAL;
using SocialCircle.Models;
using SocialCircle.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace SocialCircle.BLL
{
    public class StoryService
    {
        private readonly StoryRepository _storyRepository;

        public StoryService (StoryRepository storyRepository)
        {
            _storyRepository = storyRepository;
        }

        public Story GetStory(int id)
        {
            var story = _storyRepository.GetStory(id);
            if (story == null) return null;
            return story;
        }

        public void Create(Story story)
        {
            if(story == null) return;
            _storyRepository.Create(story);
        }

        public void Update (Story story)
        {
            _storyRepository.updateStory(story);
        }

        public void Delete (int id)
        {
            var story = _storyRepository.GetStory(id);
            if(story == null) return;
            _storyRepository.deleteStory(id);
        }
    }
}

using SocialCircle.Models;

namespace SocialCircle.Models.ViewModels
{
    public class HomeViewModel
    {
        public List<Post> Posts { get; set; } = new List<Post>();
        public List<Story> Stories { get; set; } = new List<Story>();
    }

}

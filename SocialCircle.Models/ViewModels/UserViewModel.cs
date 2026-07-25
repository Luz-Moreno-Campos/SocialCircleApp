using SocialCircle.Models;

namespace SocialCircle.Models.ViewModels
{
    public class UserViewModel
    {

        public User User { get; set; }
        public List<Post> Posts { get; set; }
        public List<User> Followers { get; set; }
        public List<User> Following { get; set; }
    }
}


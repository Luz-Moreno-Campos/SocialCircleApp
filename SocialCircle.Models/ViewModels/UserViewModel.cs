using SocialCircle.Models;

namespace SocialCircle.Models.ViewModels
{
    public class UserViewModel
    {
   
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string Initials { get; set; }
        public string Bio { get; set; }

      
        public int PostCount { get; set; }
        public int FollowersCount { get; set; }
        public int FollowingCount { get; set; }

     
        public List<Post> Posts { get; set; }
        public List<User> Followers { get; set; }
        public List<User> Following { get; set; }
    }
}


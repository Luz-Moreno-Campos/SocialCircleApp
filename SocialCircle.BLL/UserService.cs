using SocialCircle.DAL;
using SocialCircle.Models;
using SocialCircle.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace SocialCircle.BLL
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
       // private readonly PostRepository _postRepository;
        private readonly FollowRepository _followRepository;

        public UserService(UserRepository userRepository,
                          // PostRepository postRepository,
                           FollowRepository followRepository)
        {
            _userRepository = userRepository;
           // _postRepository = postRepository;
            _followRepository = followRepository;
        }

        public User ValidateLogin(string username, string password)
        {
            return _userRepository.ValidateLogin(username, password);
        }

        private string GetInitial(string username)
        {
            return username.Substring(0, 1).ToUpper();
        }

        public UserViewModel GetProfile(long userId)
        {
            var user = _userRepository.GetUserById((int)userId);

            if (user == null)
                return null;

            //var posts = _postRepository.GetPostsByUser(userId);
            var followers = _followRepository.GetFollowers(userId);
            var following = _followRepository.GetFollowing(userId);

            return new UserViewModel
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Initials = GetInitial(user.UserName),
                Bio = user.Bio,

                //PostCount = posts.Count(),
                FollowersCount = followers.Count(),
                FollowingCount = following.Count(),

                //Posts = posts.ToList(),
                Followers = followers.ToList(),
                Following = following.ToList()
            };
        }

        public List<User> GetFollowersService(long userId)
        {
            return _followRepository.GetFollowers(userId);
        }

        public List<User> GetFollowingService(long userId)
        {
            return _followRepository.GetFollowing(userId);
        }
    }
}

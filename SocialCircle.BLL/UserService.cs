using SocialCircle.DAL;
using SocialCircle.Models;
using SocialCircle.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace SocialCircle.BLL
{
    public class UserService
    {
        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User ValidateLogin(string username, string password)
        {
            return _userRepository.ValidateLogin(username, password);
        }


        public UserViewModel GetProfile(long userId)
        {
            var user = _userRepository.GetUserById((int)userId);
            var posts = _userRepository.GetUserPosts((int)userId);
            var followers = _userRepository.GetFollowers(userId);
            var following = _userRepository.GetFollowing(userId);

            return new UserViewModel
            {
                User = user,
                Posts = posts,
                Followers = followers,
                Following = following
            };
        }

        public List<User> GetFollowersService(long userId)
        {
            return _userRepository.GetFollowers(userId);
        }

        public List<User> GetFollowingService(long userId)
        {
            return _userRepository.GetFollowing(userId);
        }



    }

}

using SocialCircle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialCircle.DAL
{
    public class UserRepository
    {
        private readonly SocialCircleContext _context;

        public UserRepository(SocialCircleContext context)
        {
            _context = context;
        }


        public User ValidateLogin(string username, string password)
        {
            return _context.Users
                .FirstOrDefault(u => u.UserName == username && u.Password == password);
        }

        public User GetUserById(int userId)
        {
            return _context.Users
                .FirstOrDefault(u => u.UserId == userId);
        }

        public List<Post> GetUserPosts(int userId)
        {
            return _context.Posts
                .Where(p => p.UserId == userId)
                .OrderByDescending(p => p.PostTimeStamp)
                .ToList();
        }

        public List<User> GetFollowers(long userId)
        {
            var followerIds = _context.Follows
                .Where(f => f.FollowingId == userId)
                .Select(f => f.FollowerId)
                .ToList();

            return _context.Users
                .Where(u => followerIds.Contains(u.UserId))
                .ToList();
        }

        public List<User> GetFollowing(long userId)
        {
            var followingIds = _context.Follows
                .Where(f => f.FollowerId == userId)
                .Select(f => f.FollowingId)
                .ToList();

            return _context.Users
                .Where(u => followingIds.Contains(u.UserId))
                .ToList();
        }



    }

}

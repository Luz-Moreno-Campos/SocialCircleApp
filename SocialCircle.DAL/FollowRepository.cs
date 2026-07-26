using SocialCircle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialCircle.DAL
{
    public class FollowRepository
    {

        private readonly SocialCircleContext _context;

        public FollowRepository(SocialCircleContext context)
        {
            _context = context;
        }

        public List<User> GetFollowers(long userId)
        {
            return _context.Follows
                .Where(f => f.FollowingId == userId)
                .Select(f => f.Follower)
                .ToList();
        }

        public List<User> GetFollowing(long userId)
        {
            return _context.Follows
                .Where(f => f.FollowerId == userId)
                .Select(f => f.Following)
                .ToList();
        }



    }
}

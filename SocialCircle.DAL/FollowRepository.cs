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

        public bool IsFollowing(long followerId, long targetId)
        {
            return _context.Follows
                .Any(f => f.FollowerId == followerId && f.FollowingId == targetId);
        }

        public List<User> GetFollowing(long userId)
        {
            return _context.Follows
                .Where(f => f.FollowerId == userId)
                .Select(f => f.Following)
                .ToList();
        }

        public void AddFollow(long followerId, long targetId)
        {
            var follow = new Follow
            {
                FollowerId = followerId,
                FollowingId = targetId
            };

            _context.Follows.Add(follow);
            _context.SaveChanges();
        }

        public void RemoveFollow(long followerId, long targetId)
        {
            var follow = _context.Follows
                .FirstOrDefault(f => f.FollowerId == followerId && f.FollowingId == targetId);

            if (follow != null)
            {
                _context.Follows.Remove(follow);
                _context.SaveChanges();
            }
        }




    }
}

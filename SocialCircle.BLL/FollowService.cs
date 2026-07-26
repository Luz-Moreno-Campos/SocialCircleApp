using SocialCircle.DAL;
using SocialCircle.Models;
using System.Collections.Generic;
using System.Linq;

namespace SocialCircle.BLL
{
    public class FollowService
    {
        private readonly FollowRepository _followRepository;
        private readonly SocialCircleContext _context;

        public FollowService(FollowRepository followRepository, SocialCircleContext context)
        {
            _followRepository = followRepository;
            _context = context;
        }

       
        public void Follow(long followerId, long followingId)
        {
            if (followerId == followingId)
                return;

           
            bool alreadyFollowing = _context.Follows
                .Any(f => f.FollowerId == followerId && f.FollowingId == followingId);

            if (alreadyFollowing)
                return;

            var follow = new Follow
            {
                FollowerId = followerId,
                FollowingId = followingId
            };

            _context.Follows.Add(follow);
            _context.SaveChanges();
        }

        
        public void Unfollow(long followerId, long followingId)
        {
            var relation = _context.Follows
                .FirstOrDefault(f => f.FollowerId == followerId && f.FollowingId == followingId);

            if (relation != null)
            {
                _context.Follows.Remove(relation);
                _context.SaveChanges();
            }
        }

        
        public List<User> GetFollowers(long userId)
        {
            return _followRepository.GetFollowers(userId);
        }

        
        public List<User> GetFollowing(long userId)
        {
            return _followRepository.GetFollowing(userId);
        }

        
        public bool IsFollowing(long followerId, long followingId)
        {
            return _context.Follows
                .Any(f => f.FollowerId == followerId && f.FollowingId == followingId);
        }
    }
}

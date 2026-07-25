using SocialCircle.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialCircle.BLL
{
    public class FollowService
    {
        private readonly FollowRepository _followRepository;

        public FollowService(FollowRepository followRepository)
        {
            _followRepository = followRepository;
        }
    }

}

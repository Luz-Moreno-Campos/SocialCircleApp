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

    }

}

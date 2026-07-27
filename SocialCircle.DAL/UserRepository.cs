using SocialCircle.Models;
using System.Collections.Generic;
using System.Linq;

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
            return _context.Users.FirstOrDefault(u => u.UserName == username && u.Password == password);
        }

        public User GetUserById(long userId)
        {
            return _context.Users.FirstOrDefault(u => u.UserId == userId);
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }

        public void CreateUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

    }
}

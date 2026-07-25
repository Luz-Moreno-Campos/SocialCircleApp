using SocialCircle.DAL;
using SocialCircle.Models;
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
    }

}

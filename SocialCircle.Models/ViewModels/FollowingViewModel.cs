using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialCircle.Models.ViewModels
{
    public class FollowingViewModel
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string Initials { get; set; }

        public List<User> Following { get; set; }
    }

}

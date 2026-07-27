using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialCircle.Models.ViewModels
{
    public class PostViewModel
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string AuthorName { get; set; } = string.Empty;

        public List<CommentViewModel> Comments { get; set; } = new List<CommentViewModel>();
    }
}

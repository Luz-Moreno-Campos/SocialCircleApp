using System;
using System.Collections.Generic;

namespace SocialCircle.Models;

public partial class Post
{
    public long PostId { get; set; }

    public long UserId { get; set; }

    public DateTime PostTimeStamp { get; set; }

    public string? ImageUrl { get; set; }
    public string TextContent { get; set; } = null!;

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual User User { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}

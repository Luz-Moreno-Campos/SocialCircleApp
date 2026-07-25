using System;
using System.Collections.Generic;

namespace SocialCircle.Models;

public partial class Comment
{
    public long CommentId { get; set; }

    public long UserId { get; set; }

    public long PostId { get; set; }

    public string CommentText { get; set; } = null!;

    public DateTime CommentTimeStamp { get; set; }

    public virtual Post Post { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}

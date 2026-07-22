using System;
using System.Collections.Generic;

namespace SocialCircle.MVC.Models;

public partial class Story
{
    public long StoryId { get; set; }

    public long UserId { get; set; }

    public string? StoryText { get; set; }

    public DateTime CreationTimestamp { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}

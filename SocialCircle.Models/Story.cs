using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SocialCircle.Models;

public partial class Story
{
    public long StoryId { get; set; }

    public long UserId { get; set; }

    [Required(ErrorMessage = "Input Story text")]
    public string? StoryText { get; set; }

    public DateTime CreationTimestamp { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}

using System;
using System.Collections.Generic;

namespace SocialCircle.Models;

public partial class User
{
    public long UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Bio { get; set; }

    public string? ProfilePicture { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<DirectMessage> DirectMessageReceivers { get; set; } = new List<DirectMessage>();

    public virtual ICollection<DirectMessage> DirectMessageSenders { get; set; } = new List<DirectMessage>();

    public virtual ICollection<Follow> FollowFollowers { get; set; } = new List<Follow>();

    public virtual ICollection<Follow> FollowFollowings { get; set; } = new List<Follow>();

    public virtual ICollection<Post> PostsNavigation { get; set; } = new List<Post>();

    public virtual ICollection<Story> Stories { get; set; } = new List<Story>();

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    public virtual ICollection<Story> StoriesNavigation { get; set; } = new List<Story>();
}

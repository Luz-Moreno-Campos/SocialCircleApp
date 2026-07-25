using System;
using System.Collections.Generic;

namespace SocialCircle.Models;

public partial class Follow
{
    public long FollowerId { get; set; }

    public long FollowingId { get; set; }

    public DateTime FollowStartTimestamp { get; set; }

    public virtual User Follower { get; set; } = null!;

    public virtual User Following { get; set; } = null!;
}

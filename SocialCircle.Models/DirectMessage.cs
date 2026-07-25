using System;
using System.Collections.Generic;

namespace SocialCircle.Models;

public partial class DirectMessage
{
    public long DirectMessageId { get; set; }

    public long SenderId { get; set; }

    public long ReceiverId { get; set; }

    public string MessageText { get; set; } = null!;

    public DateTime DmTimestamp { get; set; }

    public virtual User Receiver { get; set; } = null!;

    public virtual User Sender { get; set; } = null!;
}

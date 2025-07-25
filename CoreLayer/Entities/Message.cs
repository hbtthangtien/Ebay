﻿using System;
using System.Collections.Generic;

namespace CoreLayer.Entities;

public partial class Message
{
    public int Id { get; set; }

    public int? SenderId { get; set; }

    public int? ReceiverId { get; set; }

    public int? ProductId { get; set; }

    public string? Content { get; set; }

    public DateTime? Timestamp { get; set; }

    public virtual User? Receiver { get; set; }

    public virtual User? Sender { get; set; }

    public virtual Product? Product { get; set; }
}

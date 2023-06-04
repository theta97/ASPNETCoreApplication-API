using System;
using System.Collections.Generic;

namespace ASPNETCoreApplication.Entities;

public partial class ReadingHistory
{
    public int ReadingId { get; set; }

    public int ReaderId { get; set; }

    public int BookId { get; set; }

    public virtual Book Book { get; set; } = null!;

    public virtual Reader Reader { get; set; } = null!;
}

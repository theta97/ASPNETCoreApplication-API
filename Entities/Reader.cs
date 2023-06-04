using System;
using System.Collections.Generic;

namespace ASPNETCoreApplication.Entities;

public partial class Reader
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public byte[]? Profilepic { get; set; }

    public virtual ICollection<ReadingHistory> ReadingHistories { get; set; } = new List<ReadingHistory>();
}

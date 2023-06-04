using System;
using System.Collections.Generic;

namespace ASPNETCoreApplication.Entities;

public partial class Genre
{
    public int GenreId { get; set; }

    public string Genre1 { get; set; } = null!;

    public virtual ICollection<Bookgenre> Bookgenres { get; set; } = new List<Bookgenre>();
}

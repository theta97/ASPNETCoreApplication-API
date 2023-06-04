using System;
using System.Collections.Generic;

namespace ASPNETCoreApplication.Entities;

public partial class Bookgenre
{
    public int Bgid { get; set; }

    public int Genreid { get; set; }

    public int Bookid { get; set; }

    public virtual Book Book { get; set; } = null!;

    public virtual Genre Genre { get; set; } = null!;
}

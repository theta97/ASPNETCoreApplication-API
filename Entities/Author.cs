﻿using System;
using System.Collections.Generic;

namespace ASPNETCoreApplication.Entities;

public partial class Author
{
    public int AuthorId { get; set; }

    public string Authorname { get; set; } = null!;

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}

using System;
using System.Collections.Generic;

namespace ASPNETCoreApplication.Entities;

public partial class Book
{
    public int BookId { get; set; }

    public string Bookname { get; set; } = null!;

    public int AuthorId { get; set; }

    public string Bookpic { get; set; } = null!;

    public string Bookpdf { get; set; } = null!;

    public virtual Author Author { get; set; } = null!;

    public virtual ICollection<Bookgenre> Bookgenres { get; set; } = new List<Bookgenre>();

    public virtual ICollection<ReadingHistory> ReadingHistories { get; set; } = new List<ReadingHistory>();
}

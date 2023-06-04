using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

namespace ASPNETCoreApplication.Entities;

public partial class BookReaderContext : DbContext
{
    public BookReaderContext()
    {
    }

    public BookReaderContext(DbContextOptions<BookReaderContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Bookgenre> Bookgenres { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Reader> Readers { get; set; }

    public virtual DbSet<ReadingHistory> ReadingHistories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("server=127.0.0.1;port=3306;user=root;password=My@sql-coder123;database=book_reader");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.AuthorId).HasName("PRIMARY");

            entity.ToTable("author");

            entity.Property(e => e.AuthorId).HasColumnName("Author_ID");
            entity.Property(e => e.Authorname)
                .HasMaxLength(255)
                .HasColumnName("authorname");
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.BookId).HasName("PRIMARY");

            entity.ToTable("books");

            entity.HasIndex(e => e.AuthorId, "author_id");

            entity.Property(e => e.BookId).HasColumnName("Book_ID");
            entity.Property(e => e.AuthorId).HasColumnName("author_id");
            entity.Property(e => e.Bookname)
                .HasMaxLength(255)
                .HasColumnName("bookname");
            entity.Property(e => e.Bookpdf)
                .HasColumnType("text")
                .HasColumnName("bookpdf");
            entity.Property(e => e.Bookpic)
                .HasColumnType("text")
                .HasColumnName("bookpic");

            entity.HasOne(d => d.Author).WithMany(p => p.Books)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("author_id");
        });

        modelBuilder.Entity<Bookgenre>(entity =>
        {
            entity.HasKey(e => e.Bgid).HasName("PRIMARY");

            entity.ToTable("bookgenre");

            entity.HasIndex(e => e.Bookid, "bookid");

            entity.HasIndex(e => e.Genreid, "genreid");

            entity.Property(e => e.Bgid).HasColumnName("BGID");
            entity.Property(e => e.Bookid).HasColumnName("bookid");
            entity.Property(e => e.Genreid).HasColumnName("genreid");

            entity.HasOne(d => d.Book).WithMany(p => p.Bookgenres)
                .HasForeignKey(d => d.Bookid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("bookid");

            entity.HasOne(d => d.Genre).WithMany(p => p.Bookgenres)
                .HasForeignKey(d => d.Genreid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("genreid");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.GenreId).HasName("PRIMARY");

            entity.ToTable("genre");

            entity.Property(e => e.GenreId).HasColumnName("Genre_ID");
            entity.Property(e => e.Genre1)
                .HasMaxLength(255)
                .HasColumnName("genre");
        });

        modelBuilder.Entity<Reader>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("readers");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Profilepic).HasColumnName("profilepic");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .HasColumnName("username");
        });

        modelBuilder.Entity<ReadingHistory>(entity =>
        {
            entity.HasKey(e => e.ReadingId).HasName("PRIMARY");

            entity.ToTable("reading_history");

            entity.HasIndex(e => e.BookId, "book_id");

            entity.HasIndex(e => e.ReaderId, "reader_id");

            entity.Property(e => e.ReadingId).HasColumnName("reading_id");
            entity.Property(e => e.BookId).HasColumnName("book_id");
            entity.Property(e => e.ReaderId).HasColumnName("reader_id");

            entity.HasOne(d => d.Book).WithMany(p => p.ReadingHistories)
                .HasForeignKey(d => d.BookId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("book_id");

            entity.HasOne(d => d.Reader).WithMany(p => p.ReadingHistories)
                .HasForeignKey(d => d.ReaderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("reader_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

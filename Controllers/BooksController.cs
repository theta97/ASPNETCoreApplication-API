using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASPNETCoreApplication.Entities;
using Microsoft.Extensions.FileProviders;

namespace ASPNETCoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookReaderContext _context;
        private readonly IWebHostEnvironment _environment;

        public BooksController(BookReaderContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
          if (_context.Books == null)
          {
              return NotFound();
          }
            
            //return await _context.Books.ToListAsync();
            var booklist = await _context.Books.ToListAsync();
            if(booklist != null && booklist.Count > 0)  {
                booklist.ForEach(book =>
                {
                    book.Bookpic = GetImagebyBookID(book.BookId, book.Bookpic);
                });
            }
            return booklist;
        }

        [HttpGet]
        private string GetFilePath(int BookID, string BookName)
        {
           
            return this._environment.WebRootPath + "\\images\\imgtst\\image.jpg";
        }

        [HttpGet]
        private  string GetImagebyBookID(int bookid, string bookpic)
        { 
            string ImageURL = string.Empty;
            string HostURL = "https://10.0.2.2:7128";
            string filepath = GetFilePath(bookid,bookpic);
            string imagepath = filepath;
            if(!System.IO.File.Exists(imagepath))
            {
                ImageURL = HostURL + "/images/imgtst/image.jpg";
            }
            else
            {
                ImageURL = HostURL+"/images/"+bookpic;
            }
            return ImageURL;

        }

        //public void Configure(IApplicationBuilder app)
        //{
        //    app.UseStaticFiles(); // For the wwwroot folder

        //    app.UseStaticFiles(new StaticFileOptions()
        //    {
        //        FileProvider = new PhysicalFileProvider(
        //                            Path.Combine(Directory.GetCurrentDirectory(), @"Images")),
        //        RequestPath = new PathString("/app-images")
        //    });
        //}

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
          if (_context.Books == null)
          {
              return NotFound();
          }
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, Book book)
        {
            if (id != book.BookId)
            {
                return BadRequest();
            }

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
          if (_context.Books == null)
          {
              return Problem("Entity set 'BookReaderContext.Books'  is null.");
          }
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBook", new { id = book.BookId }, book);
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            if (_context.Books == null)
            {
                return NotFound();
            }
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookExists(int id)
        {
            return (_context.Books?.Any(e => e.BookId == id)).GetValueOrDefault();
        }
    }
}

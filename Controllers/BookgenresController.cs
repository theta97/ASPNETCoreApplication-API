using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASPNETCoreApplication.Entities;

namespace ASPNETCoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookgenresController : ControllerBase
    {
        private readonly BookReaderContext _context;

        public BookgenresController(BookReaderContext context)
        {
            _context = context;
        }

        // GET: api/Bookgenres
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bookgenre>>> GetBookgenres()
        {
          if (_context.Bookgenres == null)
          {
              return NotFound();
          }
            return await _context.Bookgenres.ToListAsync();
        }

        // GET: api/Bookgenres/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bookgenre>> GetBookgenre(int id)
        {
          if (_context.Bookgenres == null)
          {
              return NotFound();
          }
            var bookgenre = await _context.Bookgenres.FindAsync(id);

            if (bookgenre == null)
            {
                return NotFound();
            }

            return bookgenre;
        }

        // PUT: api/Bookgenres/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookgenre(int id, Bookgenre bookgenre)
        {
            if (id != bookgenre.Bgid)
            {
                return BadRequest();
            }

            _context.Entry(bookgenre).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookgenreExists(id))
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

        // POST: api/Bookgenres
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Bookgenre>> PostBookgenre(Bookgenre bookgenre)
        {
          if (_context.Bookgenres == null)
          {
              return Problem("Entity set 'BookReaderContext.Bookgenres'  is null.");
          }
            _context.Bookgenres.Add(bookgenre);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBookgenre", new { id = bookgenre.Bgid }, bookgenre);
        }

        // DELETE: api/Bookgenres/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookgenre(int id)
        {
            if (_context.Bookgenres == null)
            {
                return NotFound();
            }
            var bookgenre = await _context.Bookgenres.FindAsync(id);
            if (bookgenre == null)
            {
                return NotFound();
            }

            _context.Bookgenres.Remove(bookgenre);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookgenreExists(int id)
        {
            return (_context.Bookgenres?.Any(e => e.Bgid == id)).GetValueOrDefault();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibrarySystem.Api.Data;
using LibrarySystem.Api.Entities;

namespace LibrarySystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly LibraryDbContext _context;

        public BooksController(LibraryDbContext context)
        {
            _context = context;
        }

        // GET: api/Books (Összes könyv lekérdezése - READ)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            return await _context.Books.ToListAsync();
        }

        // GET: api/Books/5 (Egy adott könyv lekérdezése leltári szám alapján - READ)
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound("A keresett könyv nem található.");
            }

            return book;
        }

        // POST: api/Books (Új könyv felvétele - CREATE)
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
            // Az Entity Framework Model Validáció itt automatikusan lefut!
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            // Visszaadja a sikeresen létrehozott könyvet és a hivatkozását
            return CreatedAtAction(nameof(GetBook), new { id = book.BookNumber }, book);
        }

        // PUT: api/Books/5 (Meglévő könyv módosítása - UPDATE)
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, Book book)
        {
            if (id != book.BookNumber)
            {
                return BadRequest("Az URL-ben lévő ID nem egyezik a könyv adatlapján lévővel.");
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
                    return NotFound("A módosítani kívánt könyv nem található.");
                }
                else
                {
                    throw;
                }
            }

            return NoContent(); // Sikeres módosítás esetén nincs visszatérési adat, csak egy 204-es státuszkód
        }

        // DELETE: api/Books/5 (Könyv törlése - DELETE)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Segédmetódus a módosításhoz
        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.BookNumber == id);
        }
    }
}
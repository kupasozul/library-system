using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibrarySystem.Api.Data;
using LibrarySystem.Api.Entities;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly LibraryDbContext _context;
    public BooksController(LibraryDbContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Book>>> GetBooks() => await _context.Books.ToListAsync();

    [HttpGet("available")]
    public async Task<ActionResult<IEnumerable<Book>>> GetAvailableBooks() 
        => await _context.Books.Where(b => !b.IsBorrowed).ToListAsync();

    [HttpPost]
    public async Task<ActionResult<Book>> PostBook(Book book)
    {
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetBooks), new { id = book.InventoryNumber }, book);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutBook(int id, Book book)
    {
        if (id != book.InventoryNumber) return BadRequest();
        _context.Entry(book).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null) return NotFound();
        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
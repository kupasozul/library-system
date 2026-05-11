using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibrarySystem.Api.Data;
using LibrarySystem.Api.Entities;

namespace LibrarySystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoansController : ControllerBase
    {
        private readonly LibraryDbContext _context;

        public LoansController(LibraryDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Loan>>> GetLoans()
        {
            var loans = await _context.Loans.ToListAsync();

            foreach (var loan in loans)
            {
                loan.PenaltyFee = CalculatePenalty(loan.ReturnDeadline);
            }

            return loans;
        }

        [HttpGet("reader/{readerNumber}")]
        public async Task<ActionResult<IEnumerable<Loan>>> GetReaderLoans(int readerNumber)
        {
            var loans = await _context.Loans
                .Where(l => l.ReaderNumber == readerNumber)
                .ToListAsync();

            foreach (var loan in loans)
            {
                loan.PenaltyFee = CalculatePenalty(loan.ReturnDeadline);
            }

            return loans;
        }

        [HttpPost]
        public async Task<ActionResult<Loan>> PostLoan(Loan loan)
        {
            if (loan.LoanDate.Date < DateTime.Today) return BadRequest("Érvénytelen dátum.");

            var book = await _context.Books.FirstOrDefaultAsync(b => b.InventoryNumber == loan.BookInventoryNumber);

            if (book == null) return NotFound("A könyv nem található.");
            if (book.IsBorrowed) return BadRequest("Ez a könyv már ki van kölcsönözve.");

            book.IsBorrowed = true;

            _context.Loans.Add(loan);
            await _context.SaveChangesAsync();

            return Ok(loan);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> ReturnBook(int id)
        {
            var loan = await _context.Loans.FirstOrDefaultAsync(l => l.LoanId == id);

            if (loan == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FirstOrDefaultAsync(b => b.InventoryNumber == loan.BookInventoryNumber);
            if (book != null)
            {
                book.IsBorrowed = false;
            }

            _context.Loans.Remove(loan);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private decimal CalculatePenalty(DateTime returnDeadline)
        {
            if (DateTime.Now <= returnDeadline) return 0;

            int delayDays = (DateTime.Now - returnDeadline).Days;
            decimal baseFee = 100;
            int multiplier;

            if (delayDays <= 10) multiplier = 1;
            else if (delayDays <= 15) multiplier = 2;
            else multiplier = 3;

            return baseFee * delayDays * multiplier;
        }
    }
}

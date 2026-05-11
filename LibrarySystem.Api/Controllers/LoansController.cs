using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibrarySystem.Api.Data;
using LibrarySystem.Api.Entities;

namespace LibrarySystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoansController : ControllerBase
    {
        private readonly LibraryDbContext _context;
        private readonly int _baseFee = 100;

        public LoansController(LibraryDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Loan>>> GetLoans()
        {
            var loans = await _context.Loans
                .Include(l => l.Book)
                .Include(l => l.Reader)
                .ToListAsync();

            foreach (var loan in loans)
            {
                loan.LateFee = CalculateLateFee(loan.ReturnDeadline);
            }

            return loans;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Loan>> GetLoan(int id)
        {
            var loan = await _context.Loans
                .Include(l => l.Book)
                .Include(l => l.Reader)
                .FirstOrDefaultAsync(l => l.LoanId == id);

            if (loan == null)
            {
                return NotFound();
            }

            loan.LateFee = CalculateLateFee(loan.ReturnDeadline);

            return loan;
        }

        [HttpPost]
        public async Task<ActionResult<Loan>> PostLoan(Loan loan)
        {
            _context.Loans.Add(loan);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLoan), new { id = loan.LoanId }, loan);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutLoan(int id, Loan loan)
        {
            if (id != loan.LoanId)
            {
                return BadRequest();
            }

            _context.Entry(loan).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoanExists(id))
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLoan(int id)
        {
            var loan = await _context.Loans.FindAsync(id);
            if (loan == null)
            {
                return NotFound();
            }

            _context.Loans.Remove(loan);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LoanExists(int id)
        {
            return _context.Loans.Any(e => e.LoanId == id);
        }

        private int CalculateLateFee(DateTime returnDeadline)
        {
            var today = DateTime.Today;
            if (today <= returnDeadline.Date) return 0;

            int daysLate = (today - returnDeadline.Date).Days;
            int multiplier = 1;

            if (daysLate >= 1 && daysLate <= 10) multiplier = 1;
            else if (daysLate >= 11 && daysLate <= 15) multiplier = 2;
            else if (daysLate >= 16) multiplier = 3;

            return _baseFee * daysLate * multiplier;
        }
    }
}
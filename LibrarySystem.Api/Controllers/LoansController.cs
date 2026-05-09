using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibrarySystem.Api.Data;
using LibrarySystem.Api.Entities;

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
        
        // Késedelmi díj kiszámítása minden elemnél a Backend oldalon
        foreach (var loan in loans)
        {
            loan.PenaltyFee = CalculatePenalty(loan.ReturnDeadline);
        }
        
        return loans;
    }

    [HttpPost]
    public async Task<ActionResult<Loan>> PostLoan(Loan loan)
    {
        // Validáció: a kölcsönzés ideje nem lehet korábbi a jelenlegi napnál
        if (loan.LoanDate.Date < DateTime.Today) return BadRequest("Érvénytelen dátum."); 
        _context.Loans.Add(loan);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetLoans), new { id = loan.LoanId }, loan);
    }

    private decimal CalculatePenalty(DateTime returnDeadline)
    {
        if (DateTime.Now <= returnDeadline) return 0;

        int delayDays = (DateTime.Now - returnDeadline).Days;
        decimal baseFee = 100; // Tetszőleges alapdíj [cite: 38]
        int multiplier = 1;

        // Szorzók a PDF alapján
        if (delayDays <= 10) multiplier = 1;
        else if (delayDays <= 15) multiplier = 2; 
        else multiplier = 3; 

        return baseFee * delayDays * multiplier; 
    }
}
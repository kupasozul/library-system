using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.Client.Models
{
    public class Loan
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Az olvasó kiválasztása kötelező!")]
        public int ReaderNumber { get; set; }

        [Required(ErrorMessage = "A könyv kiválasztása kötelező!")]
        public int BookInventoryNumber { get; set; }

        [Required(ErrorMessage = "A kölcsönzés ideje kötelező!")]
        [CustomValidation(typeof(Loan), nameof(ValidateLoanDate))]
        public DateTime LoanDate { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "A visszahozási határidő kötelező!")]
        [CustomValidation(typeof(Loan), nameof(ValidateReturnDate))]
        public DateTime ReturnDate { get; set; } = DateTime.Today.AddDays(14);
        
        public decimal PenaltyFee { get; set; }

        // Egyedi validáció: Nem lehet a jelenlegi napnál korábbi
        public static ValidationResult? ValidateLoanDate(DateTime date, ValidationContext context)
        {
            if (date.Date < DateTime.Today)
            {
                return new ValidationResult("A kölcsönzés ideje nem lehet a mai napnál korábbi!");
            }
            return ValidationResult.Success;
        }

        // Egyedi validáció: A visszahozás ideje később kell legyen, mint a kikölcsönzés ideje
        public static ValidationResult? ValidateReturnDate(DateTime returnDate, ValidationContext context)
        {
            var instance = context.ObjectInstance as Loan;
            if (instance != null && returnDate.Date <= instance.LoanDate.Date)
            {
                return new ValidationResult("A visszahozási határidőnek a kölcsönzés ideje utáni dátumnak kell lennie!");
            }
            return ValidationResult.Success;
        }
    }
}
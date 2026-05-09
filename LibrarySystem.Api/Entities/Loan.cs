using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibrarySystem.Api.Entities
{
    public class Loan : IValidatableObject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LoanId { get; set; }

        [Required]
        public int ReaderNumber { get; set; }

        [Required]
        public int BookNumber { get; set; }

        [Required(ErrorMessage = "A kölcsönzés ideje kötelező.")]
        public DateTime LoanDate { get; set; }

        [Required(ErrorMessage = "A határidő kötelező.")]
        public DateTime ReturnDeadline { get; set; }
        
        // Ez a mező nem kerül az adatbázisba, de az API visszaadja 
        [NotMapped]
        public decimal PenaltyFee { get; set; }

        public Reader? Reader { get; set; }
        public Book? Book { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (LoanDate.Date < DateTime.Today)
            {
                yield return new ValidationResult(
                    "A kölcsönzés ideje nem lehet korábbi a mainál.",
                    new[] { nameof(LoanDate) });
            }

            if (ReturnDeadline <= LoanDate)
            {
                yield return new ValidationResult(
                    "A visszahozási határidőnek későbbinek kell lennie a kölcsönzés idejénél.",
                    new[] { nameof(ReturnDeadline) });
            }
        }
    }
}

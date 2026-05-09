using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.Client.Models
{
    public class Reader
    {
        public int ReaderNumber { get; set; }

        [Required(ErrorMessage = "Az olvasó neve nem maradhat üresen!")]
        [RegularExpression(@"^(?!\s*$).+", ErrorMessage = "A név nem állhat csak szóközökből!")] //
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "A lakcím megadása kötelező!")]
        [RegularExpression(@"^(?!\s*$).+", ErrorMessage = "A lakcím nem állhat csak szóközökből!")] //
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "A születési dátum megadása kötelező!")]
        [CustomValidation(typeof(Reader), nameof(ValidateBirthDate))]
        public DateTime BirthDate { get; set; } = DateTime.Today.AddYears(-20);
        
        public static ValidationResult? ValidateBirthDate(DateTime date, ValidationContext context)
        {
            if (date.Year < 1900)
            {
                return new ValidationResult("A születési dátum éve nem lehet 1900-nál korábbi!");
            }
            return ValidationResult.Success;
        }
    }
}
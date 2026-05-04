using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibrarySystem.Api.Entities
{
    public class Reader
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReaderNumber { get; set; }

        [Required(ErrorMessage = "Az olvasó neve kötelező.")]
        [RegularExpression(@"^(?!\s*$).+", ErrorMessage = "A név nem lehet csak whitespace.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "A lakcím kötelező.")]
        [RegularExpression(@"^(?!\s*$).+", ErrorMessage = "A lakcím nem lehet csak whitespace.")]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "A születési dátum kötelező.")]
        [Range(typeof(DateTime), "1900-01-01", "2099-12-31", ErrorMessage = "A születési dátum nem lehet régebbi mint 1900")]
        public DateTime DateOfBirth { get; set; }
    }
}
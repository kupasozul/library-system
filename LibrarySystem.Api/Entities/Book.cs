using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibrarySystem.Api.Entities
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InventoryNumber { get; set; }

        [Required(ErrorMessage = "A könyv címe kötelező.")]
        [RegularExpression(@"^(?!\s*$).+", ErrorMessage = "A cím nem lehet csak whitespace.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "A szerző megadása kötelező.")]
        [RegularExpression(@"^(?!\s*$).+", ErrorMessage = "A szerző neve nem lehet csak whitespace.")]
        public string Author { get; set; } = string.Empty;

        [Required(ErrorMessage = "A kiadó megadása kötelező.")]
        [RegularExpression(@"^(?!\s*$).+", ErrorMessage = "A kiadó neve nem lehet csak whitespace.")]
        public string Publisher { get; set; } = string.Empty;

        [Required(ErrorMessage = "A kiadás éve kötelező.")]
        [Range(0, int.MaxValue, ErrorMessage = "A kiadás éve nem lehet negatív.")]
        public int ReleaseYear { get; set; }
        public bool IsBorrowed { get; set; }
    }
}

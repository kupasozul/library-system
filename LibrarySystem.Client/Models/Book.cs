using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.Client.Models
{
    public class Book
    {
        public int InventoryNumber { get; set; }

        [Required(ErrorMessage = "A könyv címe nem maradhat üresen!")]
        [RegularExpression(@"^(?!\s*$).+", ErrorMessage = "A cím nem állhat csak szóközökből!")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "A szerző megadása kötelező!")]
        [RegularExpression(@"^(?!\s*$).+", ErrorMessage = "A szerző neve nem állhat csak szóközökből!")]
        public string Author { get; set; } = string.Empty;

        [Required(ErrorMessage = "A kiadó megadása kötelező!")]
        [RegularExpression(@"^(?!\s*$).+", ErrorMessage = "A kiadó neve nem állhat csak szóközökből!")]
        public string Publisher { get; set; } = string.Empty;

        [Required(ErrorMessage = "A kiadás éve kötelező!")]
        [Range(0, 2100, ErrorMessage = "A kiadás éve nem lehet negatív szám!")] //
        public int ReleaseYear { get; set; }
        
        public bool IsBorrowed { get; set; }
    }
}
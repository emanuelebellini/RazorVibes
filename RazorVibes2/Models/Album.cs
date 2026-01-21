using System;
using System.ComponentModel.DataAnnotations;

namespace RazorVibes2.Models
{
    public class Album
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [StringLength(100)]
        public string Artist { get; set; }

        [Required]
        [StringLength(50)]
        public string Genre { get; set; }

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [Range(0.0, 999.99)]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace RazorVibes.Models;

public class Song
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Artist { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string Genre { get; set; } = string.Empty;

    [Display(Name = "Release Date")]
    [DataType(DataType.Date)]
    public DateTime ReleasedOn { get; set; } = DateTime.Today;

    [Range(1, 10)]
    public int Rating { get; set; } = 5;
}

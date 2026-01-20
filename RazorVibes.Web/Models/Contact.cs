using System;
using System.ComponentModel.DataAnnotations;

namespace RazorVibes.Web.Models
{
    public class Contact
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(200)]
        public string Notes { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}

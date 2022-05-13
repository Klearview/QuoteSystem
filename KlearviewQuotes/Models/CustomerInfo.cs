using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace KlearviewQuotes.Models
{
    public class CustomerInfo
    {
        [Required]
        [Display(Name = "Customer Name")]
        public string? Name { get; set; }

        [Required]
        public string? Address { get; set; }

        [Phone]
        public string? Phone { get; set; }

        [Phone]
        public string? Cell { get; set; }

        [Phone]
        public string? Work { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

    }
}

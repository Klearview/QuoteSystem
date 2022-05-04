using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace QuoteSystem.Models
{
    public class CustomerInfo
    {
        [Required]
        [Display(Name = "Customer Name")]
        public string? Name { get; set; }

        [Required]
        public string? Address { get; set; }

        [Required]
        public string? City { get; set; }

        [Required]
        [Display(Name = "Postal Code")]
        public string? PostalCode { get; set; }

        [Phone]
        public string? Phone { get; set; }

        [Phone]
        public string? Cell { get; set; }

        [Phone]
        public string? Work { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [XmlIgnore]
        [Display(Name = "Full Address")]
        public string? FullAddress
        {
            get
            {
                return $"{Address} {City}, {PostalCode}";
            }
        }
    }
}

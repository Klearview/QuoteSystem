using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace KlearviewQuotes.Models
{
    public class CustomerInfo
    {
        [Required]
        [Display(Name = "Customer Name")]
        public string? Name { get; set; }

        [Display(Name = "Account Number")]
        public string? AccountNumber { get; set; }

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


        public bool Contains(string search)
        {
            search = search.ToLower();

            return (
                (Name != null && Name.ToLower().Contains(search))||
                (AccountNumber != null && AccountNumber.ToLower().Contains(search)) ||
                (Address != null && Address.ToLower().Contains(search)) ||
                (Phone != null && Phone.ToLower().Contains(search)) ||
                (Cell != null && Cell.ToLower().Contains(search)) ||
                (Work != null && Work.ToLower().Contains(search)) ||
                (Email != null && Email.ToLower().Contains(search))
            );
        }
    }
}

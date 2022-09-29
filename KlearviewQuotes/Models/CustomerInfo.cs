using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
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
            search = FormatString(search);

            return (
                (Name != null && FormatString(Name).Contains(search))||
                (AccountNumber != null && FormatString(AccountNumber).Contains(search)) ||
                (Address != null && FormatString(Address).Contains(search)) ||
                (Phone != null && FormatString(Phone).Contains(search)) ||
                (Cell != null && FormatString(Cell).Contains(search)) ||
                (Work != null && FormatString(Work).Contains(search)) ||
                (Email != null && FormatString(Email).Contains(search))
            );
        }

        private string FormatString(string input)
        {
            input = input.ToLower();

            string regExp = "\\W";
            return Regex.Replace(input, regExp, "");
        }
    }
}

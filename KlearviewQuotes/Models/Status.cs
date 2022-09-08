using System.ComponentModel.DataAnnotations;

namespace KlearviewQuotes.Models
{
    public class Status
    {
        [Key]
        [Required]
        public int? Id { get; set; }

        [Display(Name = "Status")]
        public string? Name { get; set; }
    }
}

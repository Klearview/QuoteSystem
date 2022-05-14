using System.ComponentModel.DataAnnotations;

namespace KlearviewQuotes.Models.Identity
{
    public class RoleModification
    {
        [Required]
        public string? RoleName { get; set; }

        [Required]
        public string? RoleId { get; set; }

        public string[]? UsersInGroup { get; set; }
    }
}

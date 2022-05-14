using System.ComponentModel.DataAnnotations;

namespace KlearviewQuotes.Models.Identity
{
    public class RoleModification
    {
        [Required]
        public string RoleName { get; set; }

        public string RoleId { get; set; }

        public Dictionary<string, bool> UsersInGroup { get; set; }
    }
}

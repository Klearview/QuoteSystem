using Microsoft.AspNetCore.Identity;

namespace KlearviewQuotes.Models.Identity
{
    public class RoleEdit
    {
        public IdentityRole Role { get; set; }

        public Dictionary<IdentityUser, bool> UsersInGroupAlwaysAdmin { get; set; }
        public Dictionary<IdentityUser, bool> UsersInGroup { get; set; }
    }
}

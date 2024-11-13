using Microsoft.AspNetCore.Identity;

namespace LinkProject.Models.Role
{
    public class Role:IdentityRole
    {
        public string Description { get; set; }
    }
}

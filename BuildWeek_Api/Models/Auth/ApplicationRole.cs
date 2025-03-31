using Microsoft.AspNetCore.Identity;

namespace BuildWeek_Api.Models.Auth
{
    public class ApplicationRole : IdentityRole
    {
        public ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}

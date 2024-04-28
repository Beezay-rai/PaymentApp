using Microsoft.AspNetCore.Identity;

namespace PaymentApp.Data
{
    public class AppUserDetail:IdentityUser
    {
        public string Role { get; set; }
        public string Password { get; set; }
    }

    public class UserRoles:IdentityRole
    {
        public string Details { get; set; }

    }
}

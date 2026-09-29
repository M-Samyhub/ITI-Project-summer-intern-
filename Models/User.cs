using Microsoft.AspNetCore.Identity;

namespace ItiFinalProject.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

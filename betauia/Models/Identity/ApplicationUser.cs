using Microsoft.AspNetCore.Identity;

namespace betauia.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            VerifiedEmail = false;
            Banned = false;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public bool VerifiedEmail { get; set; }
        public bool Banned { get; set; }
    }
}

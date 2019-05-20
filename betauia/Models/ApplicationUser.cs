using Microsoft.AspNetCore.Identity;

namespace betauia.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string NickName { get; set; }
        
        public  virtual string claimTest { get; set; }

        public bool ForceLogOut { get; set; }
    }
}
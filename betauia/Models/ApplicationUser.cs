using Microsoft.AspNetCore.Identity;

namespace betauia.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string NickName { get; set; }
        
        public  virtual string claimTest { get; set; }
    }
}
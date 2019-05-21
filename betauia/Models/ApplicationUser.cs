using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace betauia.Models
{
    public class ApplicationUser : IdentityUser
    {

        public string FullName { get; set; }
        

        public ApplicationUser()
        {
            ForceLogOut = false;
            VerifiedEmail = false;
            Active = true;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }


        public string NickName { get; set; }
        
        public bool ForceLogOut { get; set; }
        
        public bool VerifiedEmail { get; set; }
        
        public bool Active { get; set; }
    }
}
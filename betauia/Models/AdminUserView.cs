using System.Collections.Generic;

namespace betauia.Models
{
    public class AdminUserView
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string UserName { get; set; }
        public string Email { get; set; }
        public bool Banned { get; set; }
        public bool VerifiedEmail { get; set; }

        public AdminUserView()
        {

        }
        public AdminUserView(ApplicationUser user)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            UserName = user.UserName;
            Banned = user.Banned;
            VerifiedEmail = user.VerifiedEmail;
        }
    }
}

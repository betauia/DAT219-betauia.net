using System.Collections;
using System.Collections.Generic;

namespace betauia.Models
{
    public class ProfileViewModel
    {
        public string Id { get; set; }
        
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public ProfileViewModel()
        {
            
        }

        public ProfileViewModel(ApplicationUser user)
        {
            Id = user.Id;
            
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            UserName = user.UserName;
        }

        public static IEnumerable<ProfileViewModel> GetUserProfiles(IEnumerable<ApplicationUser> users)
        {
            var profiles = new List<ProfileViewModel>{};
            foreach (ApplicationUser user in users)
            {
                profiles.Add(new ProfileViewModel(user));
            }
            return profiles;
        }
    }
}
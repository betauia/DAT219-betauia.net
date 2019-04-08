using betauia.Models;
using Microsoft.AspNetCore.Identity;

namespace betauia.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext db, UserManager<ApplicationUser> um)
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            var user = new ApplicationUser { Email = "user@gmail.com", FullName = "User name", NickName = "Nickname" };
            um.CreateAsync(user, "Password1.").Wait();

            db.SaveChanges();
        }
    }
}
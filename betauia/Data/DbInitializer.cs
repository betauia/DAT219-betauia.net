using System.Collections.Generic;
using System;
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

            var user = new ApplicationUser { Email = "user@gmail.com", UserName = "user@gmail.com", FullName = "User name", NickName = "Nickname" };
            um.CreateAsync(user, "Password1.").Wait();

            db.Pages.AddRange(new List<PageModel>
            {
                new PageModel("My Page1", "Page title", "Page subtitle", "text"),
                new PageModel("My Page2", "Page title", "Page subtitle", "text"),
                new PageModel("My Page3", "Page title", "Page subtitle", "text"),
            });
            var post = new BlogPost ("Hello world", "test content",DateTime.UtcNow);
            db.Add(post);

            db.SaveChanges();
        }
    }
}
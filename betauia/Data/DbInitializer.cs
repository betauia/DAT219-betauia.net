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
                new PageModel { Name = "My Page", Title = "Page title", SubTitle = "Page subtitle", Content = "text" },
                new PageModel { Name = "My Page2", Title = "Page title2", SubTitle = "Page subtitle2", Content = "text2" },
                new PageModel { Name = "My Page3", Title = "Page title3", SubTitle = "Page subtitle3", Content = "text3" }
            });
            var post = new BlogPost ("Hello world", "test content",DateTime.UtcNow);
            db.Add(post);

            db.SaveChanges();
        }
    }
}
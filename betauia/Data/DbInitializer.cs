using System.Collections.Generic;
using System;
using System.Security.Claims;
using betauia.Models;
using Microsoft.AspNetCore.Identity;

namespace betauia.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext db, UserManager<ApplicationUser> um,RoleManager<IdentityRole> rm)
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            
            Config.AddRoles(um,rm);
            var user = new ApplicationUser { Email = "user@gmail.com", UserName = "user@gmail.com", FirstName = "User",LastName = "test", NickName = "Nickname"};
            um.CreateAsync(user, "Password1.").Wait();
            um.AddToRoleAsync(user, "Admin").Wait();
            
            db.Pages.AddRange(new List<PageModel>
            {
                new PageModel("My Page1", "Page title", "Page subtitle", "text"),
                new PageModel("My Page2", "Page title", "Page subtitle", "text"),
                new PageModel("My Page3", "Page title", "Page subtitle", "text"),
            });
            
            var post = new BlogPost ("Betalan 15#", "Lorem ipsum dolor sit amet, cu has quas civibus, legendos praesent ea nam, an per case persequeris liberavisse. Vix ad dolor referrentur, no est officiis molestiae gloriatur. Fuisset voluptua accommodare nec ne. Elit integre no sea, sit tollit essent at. Lorem docendi voluptaria duo te, deserunt pertinax appellantur cu cum. Te vel saepe quando. No has quem soleat, erant denique dissentias per at. At justo porro etiam vim, tation accusata constituto ad quo. Idque dicat graecis vis ei. Equidem assentior cu sed, ei ius sumo eius omittantur.");
            db.Add(post);
    
            post = new BlogPost ("Betalan 29#", "Lorem ipsum dolor sit amet, cu has quas civibus, legendos praesent ea nam, an per case persequeris liberavisse. Vix ad dolor referrentur, no est officiis molestiae gloriatur. Fuisset voluptua accommodare nec ne. Elit integre no sea, sit tollit essent at. Lorem docendi voluptaria duo te, deserunt pertinax appellantur cu cum. Te vel saepe quando. No has quem soleat, erant denique dissentias per at. At justo porro etiam vim, tation accusata constituto ad quo. Idque dicat graecis vis ei. Equidem assentior cu sed, ei ius sumo eius omittantur.");
            db.Add(post);
            
            post = new BlogPost ("Betalan 9999#", "Lorem ipsum dolor sit amet, cu has quas civibus, legendos praesent ea nam, an per case persequeris liberavisse. Vix ad dolor referrentur, no est officiis molestiae gloriatur. Fuisset voluptua accommodare nec ne. Elit integre no sea, sit tollit essent at. Lorem docendi voluptaria duo te, deserunt pertinax appellantur cu cum. Te vel saepe quando. No has quem soleat, erant denique dissentias per at. At justo porro etiam vim, tation accusata constituto ad quo. Idque dicat graecis vis ei. Equidem assentior cu sed, ei ius sumo eius omittantur.");
            db.Add(post);

            
            var Event = new EventModel("Betalan", "På tide med et nytt lan", "Lanet skal skje whenever",
                "Det skal skje mange events osv.", DateTime.Now, "Author", "imagepath", 0, true);
            db.Add(Event);

            
            const int numSeats = 20;

            var seatMap = new SeatMapModel(numSeats, 100);
            
            for (var i = 1; i <= numSeats; i++)
            {
                var seat = new SeatModel() {Owner = seatMap};
                db.Add(seat);
            }
            
            db.Add(seatMap);
            
            db.SaveChanges();
        }
    }
}
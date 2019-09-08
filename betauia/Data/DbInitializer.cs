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
            var user = new ApplicationUser { Email = "user@gmail.com", UserName = "ksolli", FirstName = "Kristoffer",LastName = "Solli", NickName = "Nickname"};
            um.CreateAsync(user, "Password1.").Wait();
            um.AddToRoleAsync(user, "Admin").Wait();
            
            user = new ApplicationUser { Email = "erikaa17@uia.no", UserName = "erikdakool", FirstName = "Erik",LastName = "Aspen", NickName = "Nickname"};
            um.CreateAsync(user, "Password1.").Wait();
            um.AddToRoleAsync(user, "SuperAdmin").Wait();
            
            user = new ApplicationUser { Email = "user2@gmail.com", UserName = "cmathisen", FirstName = "Christer",LastName = "Mathisen", NickName = "Nickname"};
            um.CreateAsync(user, "Password1.").Wait();
            um.AddToRoleAsync(user, "User").Wait();
            
            user = new ApplicationUser { Email = "user3@gmail.com", UserName = "eeilertsen", FirstName = "Even",LastName = "Eilertsen", NickName = "Nickname"};
            um.CreateAsync(user, "Password1.").Wait();
            um.AddToRoleAsync(user, "User").Wait();
            
            user = new ApplicationUser { Email = "user4@gmail.com", UserName = "askailand", FirstName = "Aslak",LastName = "Skailand", NickName = "Nickname"};
            um.CreateAsync(user, "Password1.").Wait();
            um.AddToRoleAsync(user, "User").Wait();
            
            user = new ApplicationUser { Email = "user5@gmail.com", UserName = "mbraaten", FirstName = "Martin",LastName = "Baraaten", NickName = "Nickname"};
            um.CreateAsync(user, "Password1.").Wait();
            um.AddToRoleAsync(user, "User").Wait();
            
            db.Pages.AddRange(new List<PageModel>
            {
                new PageModel("My Page1", "Page title", "Page subtitle", "text"),
                new PageModel("My Page2", "Page title", "Page subtitle", "text"),
                new PageModel("My Page3", "Page title", "Page subtitle", "text"),
            });
            
            var post = new BlogPost ("Betalan #19","22-24. Mars", "Lorem ipsum dolor sit amet, cu has quas civibus, legendos praesent ea nam, an per case persequeris liberavisse. Vix ad dolor referrentur, no est officiis molestiae gloriatur. Fuisset voluptua accommodare nec ne. Elit integre no sea, sit tollit essent at. Lorem docendi voluptaria duo te, deserunt pertinax appellantur cu cum. Te vel saepe quando. No has quem soleat, erant denique dissentias per at. At justo porro etiam vim, tation accusata constituto ad quo. Idque dicat graecis vis ei. Equidem assentior cu sed, ei ius sumo eius omittantur.", "");
            db.Posts.Add(post);
    
            post = new BlogPost ("Betalan #20", "18-20. Oktober","Lorem ipsum dolor sit amet, cu has quas civibus, legendos praesent ea nam, an per case persequeris liberavisse. Vix ad dolor referrentur, no est officiis molestiae gloriatur. Fuisset voluptua accommodare nec ne. Elit integre no sea, sit tollit essent at. Lorem docendi voluptaria duo te, deserunt pertinax appellantur cu cum. Te vel saepe quando. No has quem soleat, erant denique dissentias per at. At justo porro etiam vim, tation accusata constituto ad quo. Idque dicat graecis vis ei. Equidem assentior cu sed, ei ius sumo eius omittantur.", "");
            db.Posts.Add(post);
            
            post = new BlogPost ("Jobbutlysning", "ATEA søker etter studenter","Lorem ipsum dolor sit amet, cu has quas civibus, legendos praesent ea nam, an per case persequeris liberavisse. Vix ad dolor referrentur, no est officiis molestiae gloriatur. Fuisset voluptua accommodare nec ne. Elit integre no sea, sit tollit essent at. Lorem docendi voluptaria duo te, deserunt pertinax appellantur cu cum. Te vel saepe quando. No has quem soleat, erant denique dissentias per at. At justo porro etiam vim, tation accusata constituto ad quo. Idque dicat graecis vis ei. Equidem assentior cu sed, ei ius sumo eius omittantur.", "");
            db.Posts.Add(post);

            var sponsor = new SponsorModel
            {
                Id = "atea",
                Title = "Atea",
                Description = "Proud sponsor of betalan",
                Url = "atea.no"
            };
            db.Sponsors.Add(sponsor);
            
            var Event = new EventModel
            {
                Title = "Betalan",
                SubTitle = "På tide med et nytt lan",
                Description = "Lanet skal skje whenever",
                Content = "Det skal skje mange events osv.",
                IsPublic = true,
                SponsorId = "atea"
            };
            db.Add(Event);

            
/*            const int numSeats = 20;

            var seatMap = new SeatMapModel(numSeats, 100);
            
            for (var i = 1; i <= numSeats; i++)
            {
                var seat = new SeatModel() {Owner = seatMap};
                db.Seats.Add(seat);
            }
            
            db.SeatMaps.Add(seatMap);*/
            
            //var ticket = new TicketModel("20.02.92", 199, true, "Vipps");
            //var ticket2 = new TicketModel("20.02.9222", 2099, false, "PayPal");
            //db.Tickets.Add(ticket);
            //db.Tickets.Add(ticket2);
            
            db.SaveChanges();
        }
    }
}
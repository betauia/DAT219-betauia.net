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
            var claim = new Claim("AccountVerified","true",ClaimValueTypes.String);
            um.AddClaimAsync(user, claim).Wait();
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
            
/*
            const int numSeats = 20;

            var seatMap = new SeatMapModel(numSeats, 100);

            for (var i = 1; i <= numSeats; i++)
            {
                var seat = new SeatModel() {Owner = seatMap};
                db.Seats.Add(seat);
            }

            db.SeatMaps.Add(seatMap);
*/

            //var ticket = new TicketModel("20.02.92", 199, true, "Vipps");
            //var ticket2 = new TicketModel("20.02.9222", 2099, false, "PayPal");
            //db.Tickets.Add(ticket);
            //db.Tickets.Add(ticket2);

            db.SaveChanges();
        }
    }
}


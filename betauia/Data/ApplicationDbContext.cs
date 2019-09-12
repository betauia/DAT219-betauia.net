using System;
using System.Collections.Generic;
using System.Text;
using betauia.Controllers;
using betauia.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace betauia.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<PageModel> Pages { get; set; }

        public DbSet<SponsorModel> Sponsors { get; set; }
        public DbSet<BlogPost> Posts { get; set; }
        public DbSet<EventModel> Events { get; set; }
        public DbSet<EventAtendee> EventAtendees { get; set; }
        public DbSet<EventSponsor> EventSponsors { get; set; }
        public DbSet<SeatMapModel> SeatMaps { get; set; }
        public DbSet<EventSeatMap> EventSeatMaps { get; set; }
        public DbSet<EventSeat> EventSeats { get; set; }
        public DbSet<SeatModel> Seats { get; set; }
        public DbSet<TicketModel> Tickets { get; set; }

        public DbSet<ImageModel> Images { get; set; }

        public DbSet<JobApplication> JobApplications { get; set; }
    }
}

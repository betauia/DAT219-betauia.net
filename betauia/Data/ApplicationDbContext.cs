using System;
using System.Collections.Generic;
using System.Text;
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
        public DbSet<SeatMapModel> SeatMaps { get; set; }
        public DbSet<SeatModel> Seats { get; set; }
    }
}

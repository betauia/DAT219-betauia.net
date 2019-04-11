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
<<<<<<< HEAD

        public DbSet<PageModel> Pages { get; set; }
=======
        
        public DbSet<BlogPost> Posts { get; set; }
>>>>>>> 6d74ed85018f42bbd7b9d44723fd300e97c15f19
    }
}

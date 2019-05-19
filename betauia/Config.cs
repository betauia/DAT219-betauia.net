using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using betauia.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace betauia
{
    public class Config
    {
        public static void AddRoles(UserManager<ApplicationUser> um, RoleManager<IdentityRole> rm)
        {
            var newRole = new IdentityRole("User");
            rm.CreateAsync(newRole).Wait();
        }
        public static void Addpolicies(AuthorizationOptions options)
        {
            //options.AddPolicy("SuperAdminRole",policy=>policy.RequireClaim(ClaimTypes.Role,"SuperAdmin"));
            //options.AddPolicy("AdminRole", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
            //options.AddPolicy("ModRole",policy=>policy.RequireClaim(ClaimTypes.Role,"Mod"));
            //options.AddPolicy("UserRole",policy=>policy.RequireClaim(ClaimTypes.Role,"User"));
            options.AddPolicy("SuperAdmin",policy=>policy.RequireClaim("Role","SuperAdmin"));
            options.AddPolicy("Admin", policy=>policy.RequireClaim("Role","SuperAdmin","Admin"));
            options.AddPolicy("Mod",policy=>policy.RequireClaim("Role","SuperAdmin","Admin","Mod"));
            options.AddPolicy("User",policy=>policy.RequireClaim("Role"));
            
            options.AddPolicy("Blog.write", policy => policy.RequireClaim("Blog","write"));
            
            options.AddPolicy("Account.write",policy=>policy.RequireClaim("Account","write"));
            options.AddPolicy("Account.read",policy=>policy.RequireClaim("Account","read"));
            options.AddPolicy("Account.readself",policy=>policy.RequireClaim("Role"));
        }
    }
}
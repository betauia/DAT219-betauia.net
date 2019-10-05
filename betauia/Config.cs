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
            //////////////////////////
            //user role and claims////
            //////////////////////////
            var user = new IdentityRole("User");
            rm.CreateAsync(user).Wait();

            var userClaims = new List<Claim>
            {
                new Claim("Account","self",ClaimValueTypes.String),
            };

            foreach (var claim in userClaims)
            {
                rm.AddClaimAsync(user,claim).Wait();
            }

            //////////////////////////
            //mod role and claims/////
            //////////////////////////
            var mod = new IdentityRole("Mod");
            rm.CreateAsync(mod).Wait();

            var modClaims = new List<Claim>
            {
                new Claim("Blog", "write", ClaimValueTypes.String),
                new Claim("Account","read",ClaimValueTypes.String),
                new Claim("Roles","read",ClaimValueTypes.String),
                new Claim("Event","write",ClaimValueTypes.String),
                new Claim("Seatmap","read",ClaimValueTypes.String),
                new Claim("Sponsor","write",ClaimValueTypes.String),
                new Claim("Job","write",ClaimValueTypes.String),
                new Claim("Ticket","read",ClaimValueTypes.String),
                new Claim("Adminpanel","true",ClaimValueTypes.String),
            };

            foreach (var claim in userClaims)
            {
                modClaims.Add(claim);
            }

            foreach (var claim in modClaims)
            {
                rm.AddClaimAsync(mod, claim).Wait();
            }

            //////////////////////////
            //admin role and claims///
            //////////////////////////
            var admin = new IdentityRole("Admin");
            rm.CreateAsync(admin).Wait();

            var adminClaims = new List<Claim>
            {
                new Claim("Account", "write", ClaimValueTypes.String),
                new Claim("Seatmap", "write",ClaimValueTypes.String),
                new Claim("Roles", "read",ClaimValueTypes.String),
                new Claim("Claims","read",ClaimValueTypes.String),
                new Claim("Ticket","write",ClaimValueTypes.String)
            };

            foreach (var claim in modClaims)
            {
                adminClaims.Add(claim);
            }

            foreach (var claim in adminClaims)
            {
                rm.AddClaimAsync(admin, claim).Wait();
            }

            //////////////////////////
            //SuperAdmin role and claims
            //////////////////////////
            var superAdmin = new IdentityRole("SuperAdmin");
            rm.CreateAsync(superAdmin).Wait();

            var superAdminClaims = new List<Claim>
            {
              new Claim("Roles","write",ClaimValueTypes.String),
              new Claim("Claims","write",ClaimValueTypes.String)
            };

            foreach (var claim in adminClaims)
            {
                superAdminClaims.Add(claim);
            }

            foreach (var claim in superAdminClaims)
            {
                rm.AddClaimAsync(superAdmin, claim).Wait();
            }
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
            options.AddPolicy("User",policy=>policy.RequireClaim("id"));
            options.AddPolicy("Adminpanel",policy=>policy.RequireClaim("Adminpanel","true"));

            options.AddPolicy("Roles.write",policy=>policy.RequireClaim("Roles","write"));
            options.AddPolicy("Roles.read",policy=>policy.RequireClaim("Roles","read","write"));

            options.AddPolicy("Claims.read",policy=>policy.RequireClaim("Claims","read","write"));
            options.AddPolicy("Claims.write",policy=>policy.RequireClaim("Claims","write"));

            options.AddPolicy("Blog.write", policy => policy.RequireClaim("Blog","write"));
            options.AddPolicy("Event.write",policy=>policy.RequireClaim("Event","write"));
            options.AddPolicy("Sponsor.write",policy=>policy.RequireClaim("Sponsor","write"));
            options.AddPolicy("Job.write",policy=>policy.RequireClaim("Job","write"));

            options.AddPolicy("Seatmap.write",policy=>policy.RequireClaim("Seatmap","write"));
            options.AddPolicy("Seatmap.read",policy=>policy.RequireClaim("Seatmap","read","write"));

            options.AddPolicy("Account.write",policy=>policy.RequireClaim("Account","write"));
            options.AddPolicy("Account.read",policy=>policy.RequireClaim("Account","read","wrtie"));
            options.AddPolicy("Account.writeself",policy=>policy.RequireClaim("Role"));

            options.AddPolicy("EmailVerification", policy=>policy.RequireClaim("EmailVerification","true"));
            options.AddPolicy("EmailVerified", policy=>policy.RequireClaim("EmailVerified","true"));
            options.AddPolicy("PhoneVerified",policy=>policy.RequireClaim("PhoneVerified","true"));
            options.AddPolicy("AccountVerified",policy=>policy.RequireClaim("AccountVerified","true"));

            options.AddPolicy("Ticket.write",policy=>policy.RequireClaim("Ticket","write"));
            options.AddPolicy("Ticket.read",policy=>policy.RequireClaim("Ticket","read","write"));
        }
    }
}

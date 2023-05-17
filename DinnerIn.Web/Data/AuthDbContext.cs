using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DinnerIn.Web.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //Seed the rolse; user, admin and superadmin 

            var adminRoleId = "a1fe48a9-8783-4f8f-9ea1-6a03a6ba7d74";
            var superAdminRoleId = "ae72b17f-4833-428e-96c1-7ee00de714ab";
            var userRoleId = "102cee92-ae8f-4f1f-a570-a1fbf77fc941";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "Admin",
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId

                },
                new IdentityRole
                {
                    Name = "SuperAdmin",
                    NormalizedName = "SuperAdmin",
                    Id = superAdminRoleId,
                    ConcurrencyStamp = superAdminRoleId
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "User",
                    Id = userRoleId,
                    ConcurrencyStamp = userRoleId
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);

            //Seed SuperAdminUser 
            var superAdminId = "8692abcc-1349-4abf-b49e-11379f2c5669";

            var superAdminUser = new IdentityUser
            {
                UserName = "superadmin@dinnerin.com",
                Email = "superadmin@dinnerin.com",
                NormalizedEmail = "superadmin@dinnerin.com".ToUpper(),
                NormalizedUserName = "superadmin@dinnerin.com".ToUpper(),
                Id = superAdminId
            };

            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(superAdminUser, "Superadmin123");
            

            builder.Entity<IdentityUser>().HasData(superAdminUser);
            //Add all roles to superadmin
            var superAdminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId = adminRoleId,
                    UserId = superAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId = superAdminRoleId,
                    UserId = superAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId = userRoleId,
                    UserId = superAdminId
                }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);

            

        }
    }
}

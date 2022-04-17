using BusinesssTrinitySP01.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BusinesssTrinitySP01.Startup))]
namespace BusinesssTrinitySP01
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateUserAndRoles();
        }
        public void CreateUserAndRoles()
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            if (!roleManager.RoleExists("Super Admin"))
            {
                var role = new IdentityRole("Super Admin");
                roleManager.Create(role);

                var user = new ApplicationUser();
                user.UserName = "trinity@rentals.co.za";
                user.Email = "trinity@rentals.co.za";
                string pwd = "TrinityR";

                var newuser = userManager.Create(user, pwd);
                if (newuser.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Super Admin");
                }
            }

            if (!roleManager.RoleExists("Driver"))
            {
                var role = new IdentityRole("Driver");
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("Supplier"))
            {
                var role = new IdentityRole("Supplier");
                roleManager.Create(role);
            }
        }
    }
}

namespace Myth.UI.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Myth.UI.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Myth.UI.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Myth.UI.Models.ApplicationDbContext context)
        {
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            if (!context.Users.Any(x => x.UserName == "admin@v.com"))
            {
                var user = new ApplicationUser { UserName = "admin@v.com", Email = "admin@v.com" };
                userManager.Create(user, "Password5%");
                context.Roles.AddOrUpdate(x => x.Name, new ApplicationRole { Name = "admin" });
                context.SaveChanges();
                userManager.AddToRole(user.Id, "admin");
            }
        }

        //protected override void Seed(Myth.UI.Models.ApplicationDbContext context)
        //{

        //    //ApplicationDbContext context = new ApplicationDbContext();

        //    var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
        //    var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


        //    // In Startup iam creating first Admin Role and creating a default Admin User    
        //    if (!roleManager.RoleExists("Admin"))
        //    {

        //        // first we create Admin rool   
        //        var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
        //        role.Name = "Admin";
        //        roleManager.Create(role);

        //        //Here we create a Admin super user who will maintain the website                  

        //        var user = new ApplicationUser();
        //        user.UserName = "shanu";
        //        user.Email = "syedshanumcain@gmail.com";

        //        string userPWD = "A@Z200711!";

        //        var chkUser = UserManager.Create(user, userPWD);

        //        //Add default User to Role Admin   
        //        if (chkUser.Succeeded)
        //        {
        //            var result1 = UserManager.AddToRole(user.Id, "Admin");

        //        }
        //    }

        //    // creating Creating Manager role    
        //    if (!roleManager.RoleExists("Manager"))
        //    {
        //        var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
        //        role.Name = "Manager";
        //        roleManager.Create(role);

        //    }

        //    // creating Creating Employee role    
        //    if (!roleManager.RoleExists("Employee"))
        //    {
        //        var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
        //        role.Name = "Employee";
        //        roleManager.Create(role);

        //    }
        //    //// Load the user and role managers with our custom models
        //    //var userMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
        //    //var roleMgr = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

        //    //// have we loaded roles already?
        //    //if (roleMgr.RoleExists("admin"))
        //    //    return;

        //    //// create the admin role
        //    //roleMgr.Create(new IdentityRole() { Name = "admin" });

        //    //// create the default user
        //    //var user = new ApplicationUser()
        //    //{
        //    //    UserName = "admin",
        //    //    Email = "admin@admin.com",
        //    //    PasswordHash = "AB79/KrdVKWxtopvHOzx9Gblfu87uslRq9zW/yNfpgBYsiI1imraOIJbnCoT9sNM5g=="

        //    //};

        //    //// create the user with the manager class
        //    //userMgr.Create(user, "Abenine06^");

        //    //// add the user to the admin role
        //    //userMgr.AddToRole(user.Id, "admin");


        //    ////==========================2
        //    //if (!context.Roles.Any(r => r.Name == "AppAdmin"))
        //    //{
        //    //    var store = new RoleStore<IdentityRole>(context);
        //    //    var manager = new RoleManager<IdentityRole>(store);
        //    //    var role = new IdentityRole { Name = "AppAdmin" };

        //    //    manager.Create(role);
        //    //}

        //    //if (!context.Users.Any(u => u.UserName == "founder"))
        //    //{
        //    //    var store = new UserStore<ApplicationUser>(context);
        //    //    var manager = new UserManager<ApplicationUser>(store);
        //    //    var user = new ApplicationUser { UserName = "founder", Email = "founder@admin.com" };

        //    //    manager.Create(user, "ChangeItAsap!");
        //    //    manager.AddToRole(user.Id, "AppAdmin");
        //    //}



        //}
    }
}

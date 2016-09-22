namespace Security.Sample.Data.Migrations
{
    using Security.Sample.Core.Model;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Security.Sample.Data.DBContext.MisDB>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Security.Sample.Data.DBContext.MisDB context)
        {
            Role[] role = new Role[4];
             role[0] = new Role() { Name = "Admin" };
             role[1] = new Role() { Name = "Cashier", ReportingTo = role[0] };
             role[2] = new Role() { Name = "TeamLeader", ReportingTo = role[0] };
             role[3] = new Role() { Name = "Anonymous", ReportingTo = role[0] };


             context.Roles.AddOrUpdate(role);

             Role[] userRole = new Role[1];
             userRole[0] = role[0];

             User[] defaultUser = new User[1];
             User adminUser = new User { Login = "admin", Password = "75ei+bY2v3uoVZfAGx0JQruXF1XjgOxCBMseeZoY", Email = "admin@tesco.com", Phone = "0000000000", Address = "", FirstName = "Administrator", Roles = userRole.ToList(), UserStatus = true,HintQuestion="1234567890!@#$%^&*()a",HintAnswer="admin1234567890" };


             defaultUser[0] = adminUser;
             context.Users.AddOrUpdate(defaultUser);

             List<Menu> menus = new List<Menu>();

            
             Menu userMgtMenu=new Menu(){MenuName="User Account Management",ControllerName="Admin",ActionName="UserManagement"};

             Menu cashierMenu = new Menu() { MenuName = "Cashier Menu", ControllerName = "Home", ActionName = "Index" };
             Menu teamLeaderMenu = new Menu() { MenuName = "Team Leader Menu", ControllerName = "Home", ActionName = "Index" };
             Menu AnonymousMenu = new Menu() { MenuName = "Anonymous User Menu", ControllerName = "Home", ActionName = "Index" };

             menus.Add(userMgtMenu);
             menus.Add(cashierMenu);
             menus.Add(teamLeaderMenu);
             menus.Add(AnonymousMenu);

             context.Menus.AddOrUpdate(menus.ToArray());

             List<MenuMappedToRole> menuRoleMapping = new List<MenuMappedToRole>();

             menuRoleMapping.Add(new MenuMappedToRole() { Menu = userMgtMenu, Role = role[0], AccessFlag = true });
             menuRoleMapping.Add(new MenuMappedToRole() { Menu = cashierMenu, Role = role[1], AccessFlag = true });
             menuRoleMapping.Add(new MenuMappedToRole() { Menu = teamLeaderMenu, Role = role[2], AccessFlag = true });


             menuRoleMapping.Add(new MenuMappedToRole() { Menu = AnonymousMenu, Role = role[3], AccessFlag = true });
            

             context.MenuMappedToRoles.AddOrUpdate(menuRoleMapping.ToArray());
             


        }

    }
}

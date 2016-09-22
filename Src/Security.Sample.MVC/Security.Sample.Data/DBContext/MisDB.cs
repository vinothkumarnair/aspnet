using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Security.Sample.Core.Model;

namespace Security.Sample.Data.DBContext
{
    public class MisDB : DbContext
    {
        public MisDB():base("DefaultConnection")
        {
            Database.SetInitializer<MisDB>(null);
        }

        public DbSet<User> Users { get; set; }

       


        public DbSet<Role> Roles { get; set; }

        public DbSet<Menu> Menus { get; set; }

        public DbSet<MenuMappedToRole> MenuMappedToRoles { get; set; }

       

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
            .HasMany(i => i.Roles)
            .WithMany(u => u.Users)
            .Map(m =>
            {
                m.ToTable("UserRole");
                m.MapLeftKey("UserId");
                m.MapRightKey("RoleId");
            });

          
            modelBuilder.Entity<MenuMappedToRole>().HasRequired(i => i.Menu);
            modelBuilder.Entity<MenuMappedToRole>().HasRequired(i => i.Role);
            

            base.OnModelCreating(modelBuilder);
        }
    }
}

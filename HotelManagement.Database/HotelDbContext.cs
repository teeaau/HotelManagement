using System;
using System.Data.Entity;
using System.Linq;
using HotelManagement.Model;
using HotelManagement.Database.Configurations;

namespace HotelManagement.Database
{
    public class HotelDbContext : DbContext
    {
        public HotelDbContext()
            : base("name=HotelDb")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new RoleConfiguration());
            modelBuilder.Configurations.Add(new RolePermissionConfiguration());
            modelBuilder.Configurations.Add(new PermissionConfiguration());

            modelBuilder.Configurations.Add(new FloorConfiguration());
            modelBuilder.Configurations.Add(new RoomConfiguration());
            modelBuilder.Configurations.Add(new CustomerConfiguration());
            modelBuilder.Configurations.Add(new BookingConfiguration());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set;  }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Permission> Permissions { get; set; }

        public DbSet<Floor> Floors { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}
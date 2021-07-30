using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.Linq;
using HotelManagement.Model;

namespace HotelManagement.Database.Migrations
{

    internal sealed class Configuration : DbMigrationsConfiguration<HotelDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(HotelDbContext context)
        {
            foreach (var role in DefaultRoles)
            {
                context.Roles.AddOrUpdate(role);
            }
            foreach (var permission in DefaultPermissions)
            {
                context.Permissions.AddOrUpdate(permission);
            }
            foreach (var rolePermission in DefaultRolePermissions)
            {
                context.RolePermissions.AddOrUpdate(rolePermission);
            }
            foreach (var user in DefaultUsers)
            {
                context.Users.AddOrUpdate(user);
            }
            foreach (var floor in DefaultFloors)
            {
                context.Floors.AddOrUpdate(floor);
            }
            foreach (var room in DefaultRooms)
            {
                context.Rooms.AddOrUpdate(room);
            }
            context.SaveChanges();
        }
        
        ObservableCollection<Role> DefaultRoles
        {
            get
            {
                ObservableCollection<Role> roles = new ObservableCollection<Role>();
                roles.Add(new Role()
                {
                    roleId = 1,
                    roleName = "manager",
                    roleDisplay = "Giám đốc"
                });
                roles.Add(new Role()
                {
                    roleId = 2,
                    roleName = "employee",
                    roleDisplay = "Nhân viên"
                });
                return roles;
            }
        }

        ObservableCollection<Permission> DefaultPermissions
        {
            get
            {
                ObservableCollection<Permission> permissions = new ObservableCollection<Permission>();
                permissions.Add(new Permission()
                {
                    permissionId = 1, 
                    permissionName = "Booking",
                    permissionDisplay = "Đặt phòng"
                });
                permissions.Add(new Permission()
                {
                    permissionId = 2,
                    permissionName = "Payment",
                    permissionDisplay = "Thanh toán"
                });
                permissions.Add(new Permission()
                {
                    permissionId = 3,
                    permissionName = "Income",
                    permissionDisplay = "Xem thu nhập"
                });
                permissions.Add(new Permission()
                {
                    permissionId = 4,
                    permissionName = "UserManagement",
                    permissionDisplay = "Quản lý người dùng"
                });
                return permissions;
            }
        }

        ObservableCollection<User> DefaultUsers
        {
            get
            {
                ObservableCollection<User> users = new ObservableCollection<User>();
                users.Add(new User()
                {
                    userId = "admin",
                    userPassword = "123456",
                    name = "Nguyễn Văn Quốc Trọng",
                    email = "trong@gmail.com",
                    phone = "0123456789",
                    roleId = 1
                });
                return users;
            }
        }

        ObservableCollection<RolePermission> DefaultRolePermissions
        {
            get
            {
                ObservableCollection<RolePermission> rolePermissions = new ObservableCollection<RolePermission>();
                rolePermissions.Add(new RolePermission()
                {
                    roleId = 1,
                    permissionId = 1
                });
                rolePermissions.Add(new RolePermission()
                {
                    roleId = 1,
                    permissionId = 2
                });
                rolePermissions.Add(new RolePermission()
                {
                    roleId = 1,
                    permissionId = 3
                });
                rolePermissions.Add(new RolePermission()
                {
                    roleId = 1,
                    permissionId = 4
                });
                rolePermissions.Add(new RolePermission()
                {
                    roleId = 2,
                    permissionId = 1
                });
                rolePermissions.Add(new RolePermission()
                {
                    roleId = 2,
                    permissionId = 2
                });
                return rolePermissions;
            }
        }

        ObservableCollection<Floor> DefaultFloors
        {
            get
            {
                ObservableCollection<Floor> floors = new ObservableCollection<Floor>();
                for(int i = 1; i <= 5; ++i)
                {
                    floors.Add(new Floor()
                    {
                        floorId = i,
                        floorDisplay = String.Format($"Tầng {i}")           
                    });
                }
                return floors;
            }
        }

        ObservableCollection<Room> DefaultRooms
        {
            get
            {
                ObservableCollection<Room> rooms = new ObservableCollection<Room>();
                for (int i = 1; i <= 5; ++i)
                {
                    for (int j = 1; j <= 5; ++j)
                    {
                        rooms.Add(new Room()
                        {
                            floorId = i,
                            isBooked = false,
                            roomId = i * 100 + j, 
                            price = ((i % 3) + j) * 25000
                        });
                    }
                }
                return rooms;
            }
        }
    }
}

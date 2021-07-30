using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Model
{
    public class User
    {
        public string userId { get; set; }
        public string userPassword { get; set; }
        public int roleId { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public Role Role { get; set; }
    }
    public class Role
    {
        public int roleId { get; set; }
        public string roleName { get; set; }
        public string roleDisplay { get; set; }
        public ObservableCollection<User> Users { get; set; }
        public ObservableCollection<RolePermission> RolePermissions { get; set; }
    }
    public class RolePermission
    {
        public int roleId { get; set; }
        public int permissionId { get; set; }
        public Role Role { get; set; }
        public Permission Permission { get; set; }
    }
    public class Permission
    {
        public int permissionId { get; set; }
        public string permissionName { get; set; }
        public string permissionDisplay { get; set; }
        public ObservableCollection<RolePermission> RolePermissions { get; set; }
    }
}

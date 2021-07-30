using HotelManagement.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Database.Configurations
{
    public class UserConfiguration: EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            this.ToTable("User")
                .HasKey(k => k.userId)
                .HasRequired(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(r => r.roleId)
                .WillCascadeOnDelete(true);
        }
    }
    public class RoleConfiguration: EntityTypeConfiguration<Role>
    {
        public RoleConfiguration()
        {
            this.ToTable("Role")
                .HasKey(k => k.roleId)
                .Property(k => k.roleId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }
    }
    public class RolePermissionConfiguration: EntityTypeConfiguration<RolePermission>
    {
        public RolePermissionConfiguration()
        {
            this.ToTable("RolePermission")
                .HasKey(k => new { k.roleId, k.permissionId });
            this.HasRequired(rp => rp.Role)
                .WithMany(r => r.RolePermissions)
                .HasForeignKey(r => r.roleId)
                .WillCascadeOnDelete(true);
            this.HasRequired(rp => rp.Permission)
                .WithMany(p => p.RolePermissions)
                .HasForeignKey(p => p.permissionId)
                .WillCascadeOnDelete(true);
        }
    }
    public class PermissionConfiguration: EntityTypeConfiguration<Permission>
    {
        public PermissionConfiguration()
        {
            this.ToTable("Permission")
                .HasKey(k => k.permissionId)
                .Property(k => k.permissionId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None); 
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HotelManagement.UI.Views;
using HotelManagement.Model;
using HotelManagement.UI.Repositories;
using System.Collections.ObjectModel;

namespace HotelManagement.UI.ViewModels
{
    public class MainViewModel
    {
        #region Field
        MainForm view;
        LoginView vLogin;
        MenuView vMenu;
        User userLogin;
        UserRepository userRepository;
        RoleRepository roleRepository;
        RolePermissionRepository rolePermissionRepository;
        PermissionRepository permissionRepository;
        ObservableCollection<User> Users;
        ObservableCollection<Role> Roles;
        ObservableCollection<RolePermission> RolePermissions;
        ObservableCollection<Permission> Permissions;
        Dictionary<User, ObservableCollection<Permission>> UserPermissions;

        #endregion

        public MainViewModel(MainForm view)
        {
            this.view = view;
            LoadAsync();
        }
        private async Task LoadAsync()
        {
            userRepository = new UserRepository();
            Users = new ObservableCollection<User>(await userRepository.GetAllAsync());
            roleRepository = new RoleRepository();
            Roles = new ObservableCollection<Role>(await roleRepository.GetAllAsync());
            rolePermissionRepository = new RolePermissionRepository();
            RolePermissions = new ObservableCollection<RolePermission>(await rolePermissionRepository.GetAllAsync());
            permissionRepository = new PermissionRepository();
            Permissions = new ObservableCollection<Permission>(await permissionRepository.GetAllAsync());
            UserPermissions = new Dictionary<User, ObservableCollection<Permission>>();

            foreach (var user in Users)
            {
                var role = Roles.First(r => r.roleId == user.roleId);
                role.RolePermissions = new ObservableCollection<RolePermission>(RolePermissions.Where(rp => rp.roleId == role.roleId));
                var permissions = new ObservableCollection<Permission>();
                foreach (var rp in role.RolePermissions)
                {
                    permissions.Add(Permissions.First(p => p.permissionId == rp.permissionId));
                }
                UserPermissions.Add(user, permissions);
            }

            TabsViewModel.TabControl_Design();
            Supporter.GetControls<Panel>(view, "pnlContent").Controls.Add(TabsViewModel.Tabs);
            Supporter.GetControls<Button>(view, "btnUser").Click += BtnUser_Click;
        }

        #region Events

        private void BtnUser_Click(object sender, EventArgs e)
        {
            ContextMenu userMenu = new ContextMenu();
            Button btn = (Button)sender;
            if (userLogin == null)
            {
                vLogin = new LoginView();
                vLogin.Location = new Point(320, 200);
                Supporter.GetControls<Button>(vLogin, "btnCloseLogin").Click += CloseLogin_Click;
                Supporter.GetControls<Button>(vLogin, "btnLogin").Click += Login_Click;
                Supporter.GetControls<Panel>(view, "pnlContent").Controls.Add(vLogin);
            }
            else
            {
                userMenu.MenuItems.Add(new MenuItem("Đăng xuất", UserLogout));
                userMenu.Show(Supporter.GetControls<Panel>(view, "pnlNav"), new Point(btn.Left, btn.Bottom));
            }
            
        }

        private void UserLogout(object sender, EventArgs e)
        {
            userLogin = null;
            Supporter.GetControls<Button>(view, "btnUser").Text = "ĐĂNG NHẬP";
            TabsViewModel.Tabs.TabPages.Clear();
            TabsViewModel.Tabs.Visible = false;
            Supporter.GetControls<Panel>(view, "pnlMenu").Controls.Remove(vMenu);
            Supporter.GetControls<Panel>(view, "pnlMenu").Visible = false;
            Supporter.GetControls<Button>(view, "btnUser").Click -= BtnUser_Click;
            LoadAsync();
        }

        private void Login_Click(object sender, EventArgs e)
        {
            var id = Supporter.GetControls<TextBox>(vLogin, "txbID").Text;
            var pass = Supporter.GetControls<TextBox>(vLogin, "txbPassword").Text;
            var user = Users.FirstOrDefault(u => u.userId == id);
            if (user != null)
            {
                if (user.userPassword == pass)
                {
                    userLogin = user;
                    Supporter.GetControls<Button>(view, "btnUser").Text = userLogin.userId;
                    Supporter.GetControls<Panel>(view, "pnlContent").Controls.Remove(vLogin);
                    vMenu = new MenuView(UserPermissions[userLogin]);
                    vMenu.Dock = DockStyle.Fill;
                    Supporter.GetControls<Panel>(view, "pnlMenu").Controls.Add(vMenu);
                    Supporter.GetControls<Panel>(view, "pnlMenu").Visible = true;
                }
                else
                {
                    Supporter.GetControls<Label>(vLogin, "lblMessage").Text = "Mật khẩu không chính xác";
                }
            }
            else
            {
                Supporter.GetControls<Label>(vLogin ,"lblMessage").Text = "Tài khoản không tồn tại";
            }
        }

        private void CloseLogin_Click(object sender, EventArgs e)
        {
            Supporter.GetControls<Panel>(view, "pnlContent").Controls.Remove(vLogin);
        }

        #endregion
    }
}

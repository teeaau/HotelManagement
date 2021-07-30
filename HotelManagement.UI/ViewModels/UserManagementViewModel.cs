using HotelManagement.Model;
using HotelManagement.UI.Repositories;
using HotelManagement.UI.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelManagement.UI.ViewModels
{
    public class UserManagementViewModel
    {
        #region Field
        UserManagementView view;
        ObservableCollection<User> Users;
        UserRepository userRepository;
        DataGridView dgvUsers;
        BindingSource source = new BindingSource();
        User selectedUser = new User();

        #endregion

        public UserManagementViewModel(UserManagementView view)
        {
            this.view = view;
            LoadAsync();
        }
        private async Task LoadAsync()
        {
            userRepository = new UserRepository();
            Users = new ObservableCollection<User>(await userRepository.GetAllAsync());
            dgvUsers = Supporter.GetControls<DataGridView>(view, "dgvUsers");
            dgvUsers.AllowUserToAddRows = false;
            source.DataSource = Users;
            dgvUsers.DataSource = source;            
            Supporter.GetControls<Button>(view, "btnAdd").Click += Add_Click;
            Supporter.GetControls<Button>(view, "btnRemove").Click += Remove_Click;
            Supporter.GetControls<Button>(view, "btnSave").Click += Save_Click;
            Supporter.GetControls<Button>(view, "btnSearch").Click += Search_ClickAsync;
        }

        #region Events

        private void Search_ClickAsync(object sender, EventArgs e)
        {
            var text = Supporter.GetControls<TextBox>(view, "txbSearch").Text;
            var uSearch = Users.Where(u => u.userId.Contains(text));
            source.DataSource = new ObservableCollection<User>(uSearch);
            source.ResetBindings(false);
        }

        private void Save_Click(object sender, EventArgs e)
        {
            userRepository.SaveAsync();
        }

        private void Remove_Click(object sender, EventArgs e)
        {
            selectedUser = (User)dgvUsers.CurrentRow.DataBoundItem;
            Users.Remove(selectedUser);
            userRepository.Remove(selectedUser);
            source.ResetBindings(false);
        }

        private void Add_Click(object sender, EventArgs e)
        {
            selectedUser = new User();
            Users.Add(selectedUser);
            userRepository.Add(selectedUser);
            source.ResetBindings(false);
        }

        #endregion
    }
}

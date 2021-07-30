using HotelManagement.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelManagement.UI.Views
{
    public partial class UserManagementView : UserControl
    {
        UserManagementViewModel viewModel;
        public UserManagementView()
        {
            InitializeComponent();
            viewModel = new UserManagementViewModel(this);
        }
    }
}

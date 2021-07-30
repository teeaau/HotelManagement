using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HotelManagement.Model;
using HotelManagement.UI.ViewModels;

namespace HotelManagement.UI.Views
{
    public partial class MenuView : UserControl
    {
        MenuViewModel viewModel;
        public MenuView(ObservableCollection<Permission> permissions)
        {
            InitializeComponent();
            viewModel = new MenuViewModel(this, permissions);
        }
    }
}

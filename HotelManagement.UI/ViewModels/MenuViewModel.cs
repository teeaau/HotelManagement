using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HotelManagement.Model;
using HotelManagement.UI.Repositories;
using HotelManagement.UI.Views;

namespace HotelManagement.UI.ViewModels
{
    public class MenuViewModel
    {
        ObservableCollection<Permission> menu;
        MenuView view;
        public MenuViewModel(MenuView view, ObservableCollection<Permission> permissions)
        {
            this.view = view;
            this.menu = permissions;
            LoadAsync();
        }
        public async Task LoadAsync()
        {
            Supporter.GetControls<Panel>(view, "pnlMenu").Controls.Clear();
            foreach (var item in menu)
            {
                Supporter.GetControls<Panel>(view, "pnlMenu").Controls.Add(DesignButton(item));
            }
        }

        Button DesignButton(Permission item)
        {
            var btn = new Button();
            btn.FlatStyle = FlatStyle.Popup;
            btn.Name = item.permissionName;
            btn.Text = item.permissionDisplay;
            btn.BackColor = Color.Transparent;
            btn.ForeColor = Color.FromArgb(234, 191, 159);
            btn.Font = new Font("Microsoft Sans Serif", 15, FontStyle.Bold);
            btn.Dock = DockStyle.Top;
            btn.Height = 50;
            btn.Click += delegate(object sender, EventArgs e) { Item_Click(sender, e, item); };
            return btn;
        }

        private void Item_Click(object sender, EventArgs e, Permission item)
        {
            TabsViewModel.Tabs.Visible = true;
            TabsViewModel.OpenTab(item);
        }
    }
}

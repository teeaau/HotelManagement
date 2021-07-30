using HotelManagement.Model;
using HotelManagement.UI.Views;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelManagement.UI.ViewModels
{
    public static class TabsViewModel
    {
        public static TabControl Tabs = new TabControl()
        {
            DrawMode = TabDrawMode.OwnerDrawFixed,
            Dock = DockStyle.Fill,
            Visible = false,
            Padding = new Point(20, 5)
        };

        public static void TabControl_Design()
        {
            Tabs.DrawItem += Tabs_DrawItem;
            Tabs.MouseDown += Tabs_MouseDown;
        }

        private static void Tabs_MouseDown(object sender, MouseEventArgs e)
        {
            for (var i = 0; i < Tabs.TabPages.Count; i++)
            {
                var tabRect = Tabs.GetTabRect(i);
                tabRect.Inflate(-2, -2);
                var imageRect = new Rectangle((tabRect.Right - 15), tabRect.Top + (tabRect.Height - 15) / 2, 15, 15);
                if (imageRect.Contains(e.Location))
                {
                    Tabs.TabPages.RemoveAt(i);
                    if (Tabs.TabPages.Count == 0)
                    {
                        Tabs.Visible = false;
                    }
                    break;
                }
            }
        }

        private static void Tabs_DrawItem(object sender, DrawItemEventArgs e)
        {
            var tabPage = Tabs.TabPages[e.Index];
            var tabRect = Tabs.GetTabRect(e.Index);

            var closeImage = new Bitmap(Properties.Resources.Close1, new Size(15, 15));
            e.Graphics.DrawImage(closeImage,
                (tabRect.Right - closeImage.Width),
                tabRect.Top + (tabRect.Height - closeImage.Height) / 2);
            TextRenderer.DrawText(e.Graphics, tabPage.Text, tabPage.Font,
                tabRect, tabPage.ForeColor, TextFormatFlags.Left);

        }

        public static void OpenTab(Permission item)
        {
            foreach (TabPage tp in Tabs.TabPages)
            {
                if (tp.Name == item.permissionName)
                {
                    Tabs.TabPages.Remove(tp);
                }
            }

            var tabPage = new TabPage();
            tabPage.Name = item.permissionName;
            tabPage.Text = item.permissionDisplay;
            switch (item.permissionName)
            {
                case "UserManagement":
                    tabPage.Controls.Add(new UserManagementView() { Dock = DockStyle.Fill });
                    break;
                case "Booking":
                    tabPage.Controls.Add(new BookingView() { Dock = DockStyle.Fill });
                    break;
                case "Income":
                    tabPage.Controls.Add(new IncomeView() { Dock = DockStyle.Fill });
                    break;
                case "Payment":
                    tabPage.Controls.Add(new PaymentView() { Dock = DockStyle.Fill });
                    break;
            }
            Tabs.TabPages.Add(tabPage);
            Tabs.SelectedTab = tabPage;
        }
    }
}

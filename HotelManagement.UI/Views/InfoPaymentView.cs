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
    public partial class InfoPaymentView : UserControl
    {
        public InfoPaymentView(string room, string customer, string total)
        {
            InitializeComponent();
            lblRoom.Text = room;
            lblCustomer.Text = customer;
            lblTotal.Text = total;
            this.Dock = DockStyle.Top;
        }
    }
}

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
    public partial class PaymentView : UserControl
    {
        PaymentViewModel viewModel;
        public PaymentView()
        {
            InitializeComponent();
            viewModel = new PaymentViewModel(this);
        }
    }
}

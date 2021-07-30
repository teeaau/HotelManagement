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
    public partial class IncomeView : UserControl
    {
        IncomeViewModel viewModel; 
        public IncomeView()
        {
            InitializeComponent();
            viewModel = new IncomeViewModel(this);
        }
    }
}

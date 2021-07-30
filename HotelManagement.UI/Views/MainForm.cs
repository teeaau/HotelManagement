using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HotelManagement.UI.ViewModels;

namespace HotelManagement.UI.Views
{
    public partial class MainForm : Form
    {
        MainViewModel viewModel;
        public MainForm()
        {
            InitializeComponent();
            viewModel = new MainViewModel(this);
        }
    }
}

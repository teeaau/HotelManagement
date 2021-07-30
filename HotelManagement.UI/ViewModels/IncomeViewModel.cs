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
    public class IncomeViewModel
    {
        #region Field
        IncomeView view;
        BookingRepository bookingRepository;
        ObservableCollection<Booking> Payments;
        ObservableCollection<Booking> FollowPayments;
        HashSet<string> years;
        int Sum;
        int currentYear;
        int currentMonth;
        int currentDay;
        #endregion

        public IncomeViewModel(IncomeView view)
        {
            this.view = view;
            LoadAsync();
        }

        private async Task LoadAsync()
        {
            bookingRepository = new BookingRepository();
            var payments = await bookingRepository.GetAllAsync();
            Payments = new ObservableCollection<Booking>(payments.Where(p => p.checkOut != null));
            FollowPayments = new ObservableCollection<Booking>();
            Supporter.GetControls<Button>(view, "btnHide").Click += Hide_Click;

            DesignCbbYear();
            DesignCbbMonth();
            DesignCbbDay();
        }

        private void Hide_Click(object sender, EventArgs e)
        {
            Supporter.GetControls<Panel>(view, "pnlDetail").Visible = false;
        }

        #region Day

        private void DesignCbbDay()
        {
            var cbbDay = Supporter.GetControls<ComboBox>(view, "cbbDay");
            cbbDay.Items.Clear();
            cbbDay.Items.Add("All");
            for (int i = 1; i <= 31; ++i)
            {
                cbbDay.Items.Add(i.ToString());
            }
            cbbDay.SelectedIndexChanged += CbbDay_SelectedIndexChanged;
        }

        private void CbbDay_SelectedIndexChanged(object sender, EventArgs e)
        {
            var index = (sender as ComboBox).SelectedIndex;
            currentDay = index;
            ShowPayments();
        }
        #endregion

        #region Month

        private void DesignCbbMonth()
        {
            var cbbMonth = Supporter.GetControls<ComboBox>(view, "cbbMonth");
            cbbMonth.Items.Clear();
            cbbMonth.Items.Add("All");
            for (int i = 1; i <= 12; ++i)
            {
                cbbMonth.Items.Add(i.ToString());
            }
            cbbMonth.SelectedIndexChanged += CbbMonth_SelectedIndexChanged;
        }

        private void CbbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            var index = (sender as ComboBox).SelectedIndex;
            currentMonth = index;
            ShowPayments();
        }

        #endregion

        #region Year

        private void DesignCbbYear()
        {
            var cbbYear = Supporter.GetControls<ComboBox>(view, "cbbYear");
            cbbYear.Items.Clear();

            years = new HashSet<string>();
            years.Add("All");
            foreach (var payment in Payments)
            {
                years.Add(payment.checkOut.Value.Year.ToString());
            }
            cbbYear.Items.AddRange(years.ToArray());
            cbbYear.SelectedIndexChanged += CbbYear_SelectedIndexChanged;
        }

        private void CbbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            var index = (sender as ComboBox).SelectedIndex;
            if (index == 0)
            {
                currentYear = 0;
            }
            else
            {
                currentYear = Convert.ToInt32(years.ElementAt(index));
            }
            ShowPayments();
        }
        #endregion

        #region Function
        private void ShowPayments()
        {
            SetFollowPayments();
            var pnlHeader = Supporter.GetControls<Panel>(view, "pnlHeader");
            pnlHeader.Controls.Clear();
            pnlHeader.Controls.Add(new InfoPaymentView("Phòng", "Khách hàng", "Thành tiền") { Dock = DockStyle.Fill });
            var pnlPayments = Supporter.GetControls<Panel>(view, "pnlPayments");
            pnlPayments.Controls.Clear();
            Sum = 0;

            foreach(var payment in FollowPayments)
            {
                var info = new InfoPaymentView(payment.roomId.ToString(), payment.customerId, payment.total.ToString());
                info.Click += delegate (object sender, EventArgs e) { Info_Click(sender, e, payment); };

                Supporter.GetControls<Label>(info, "lblRoom").Click += delegate (object sender, EventArgs e) { Info_Click(sender, e, payment); };
                Supporter.GetControls<Label>(info, "lblCustomer").Click += delegate (object sender, EventArgs e) { Info_Click(sender, e, payment); };
                Supporter.GetControls<Label>(info, "lblTotal").Click += delegate (object sender, EventArgs e) { Info_Click(sender, e, payment); };

                pnlPayments.Controls.Add(info);
                Sum += payment.total.Value;
            }
            Supporter.GetControls<Label>(view, "lblSum").Text = String.Format($"Tổng cộng: {Sum}");
        }

        private void Info_Click(object sender, object e, Booking payment)
        {
            Supporter.GetControls<Panel>(view, "pnlDetail").Visible = true;
            Supporter.GetControls<Label>(view, "lblRoom").Text = payment.roomId.ToString();
            Supporter.GetControls<Label>(view, "lblCustomer").Text = payment.customerId;
            Supporter.GetControls<Label>(view, "lblCheckIn").Text = payment.checkIn.ToString("dd/MM/yyyy HH:mm:ss");
            Supporter.GetControls<Label>(view, "lblCheckOut").Text = payment.checkOut.Value.ToString("dd/MM/yyyy HH:mm:ss");
            Supporter.GetControls<Label>(view, "lblTotal").Text = payment.total.ToString();
        }

        private void SetFollowPayments()
        {
            if(currentYear == 0)
            {
                FollowPayments = Payments;
            }
            else
            {
                if(currentMonth == 0)
                {
                    FollowPayments = new ObservableCollection<Booking>(Payments.Where(p => p.checkOut.Value.Year == currentYear));
                }
                else
                {
                    if(currentDay == 0)
                    {
                        FollowPayments = new ObservableCollection<Booking>(Payments.Where(p => p.checkOut.Value.Year == currentYear && p.checkOut.Value.Month == currentMonth));
                    }
                    else
                    {
                        FollowPayments = new ObservableCollection<Booking>(Payments.Where(p => p.checkOut.Value.Year == currentYear && p.checkOut.Value.Month == currentMonth && p.checkOut.Value.Day == currentDay));
                    }
                }
            }
        }
        #endregion
    }
}

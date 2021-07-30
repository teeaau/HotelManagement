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
    public class PaymentViewModel
    {
        #region Field
        PaymentView view;

        FloorRepository floorRepository;
        ObservableCollection<Floor> Floors;
        RoomRepository roomRepository;
        ObservableCollection<Room> Rooms;
        ObservableCollection<Room> FollowRooms;
        Room currentRoom;

        CustomerRepository customerRepository;
        ObservableCollection<Customer> Customers;
        Customer currentCustomer;

        BookingRepository bookingRepository;
        ObservableCollection<Booking> Bookings;
        Booking currentBooking;

        #endregion

        public PaymentViewModel(PaymentView view)
        {
            this.view = view;
            LoadAsync();
        }

        private async Task LoadAsync()
        {
            LoadBookingAsync();
            LoadRoomAsync();
            LoadCustomerAsync();
        }

        #region Room
        private async Task LoadRoomAsync()
        {
            roomRepository = new RoomRepository();
            Rooms = new ObservableCollection<Room>();
            var rooms = new ObservableCollection<Room>(await roomRepository.GetAllAsync());
            floorRepository = new FloorRepository();
            Floors = new ObservableCollection<Floor>(await floorRepository.GetAllAsync());
            foreach (var floor in Floors)
            {
                floor.Rooms = new ObservableCollection<Room>();
            }
            foreach(var booking in Bookings)
            {
                var room = rooms.First(r => r.roomId == booking.roomId);
                Rooms.Add(room);
                Floors.First(f => f.floorId == room.floorId).Rooms.Add(room);
            }
            Supporter.GetControls<ComboBox>(view, "cbbFloor").Items.Add("All");
            foreach (var floor in Floors)
            {
                Supporter.GetControls<ComboBox>(view, "cbbFloor").Items.Add(floor.floorDisplay);
            }
            Supporter.GetControls<ComboBox>(view, "cbbFloor").SelectedIndexChanged += Floor_SelectedIndexChanged;
        }

        private void Floor_SelectedIndexChanged(object sender, EventArgs e)
        {
            var index = Supporter.GetControls<ComboBox>(view, "cbbFloor").SelectedIndex;
            Supporter.GetControls<ComboBox>(view, "cbbRoom").Items.Clear();
            Supporter.GetControls<ComboBox>(view, "cbbRoom").Text = null;
            Supporter.GetControls<TextBox>(view, "txbRoomPrice").Text = null;
            Supporter.GetControls<Label>(view, "lblRoomName").Text = "Phòng";
            FollowRooms = (index <= 0) ? Rooms : Floors[index - 1].Rooms;
            foreach (var room in FollowRooms)
            {
                Supporter.GetControls<ComboBox>(view, "cbbRoom").Items.Add(room.roomId);
            }
            Supporter.GetControls<ComboBox>(view, "cbbRoom").SelectedIndexChanged += Room_SelectedIndexChanged;
            DesignRoomPayment();
        }

        private void Room_SelectedIndexChanged(object sender, EventArgs e)
        {
            var index = Supporter.GetControls<ComboBox>(view, "cbbRoom").SelectedIndex;
            currentRoom = FollowRooms[index];
            SetPayment();
        }

        private void DesignRoomPayment()
        {
            var pnlRooms = Supporter.GetControls<Panel>(view, "pnlRooms");
            pnlRooms.Controls.Clear();
            int i = 0, j = 0;
            foreach (var room in FollowRooms)
            {
                var rpayment = new RoomPaymentView();
                Supporter.GetControls<Label>(rpayment, "lblRoom").Text = String.Format($"Phòng {room.roomId}");
                Supporter.GetControls<Button>(rpayment, "btnPayment").Click += delegate (object sender, EventArgs e) { rpayment_Click(sender, e, room); };
                rpayment.Size = new System.Drawing.Size(150, 150);
                rpayment.Location = new System.Drawing.Point(j * 160, i * 160);
                ++j;
                i += (j / 5);
                j %= 5;
                pnlRooms.Controls.Add(rpayment);
            }
        }

        private void rpayment_Click(object sender, EventArgs e, Room room)
        {
            currentRoom = room;
            SetPayment();
            Supporter.GetControls<ComboBox>(view, "cbbRoom").Text = currentRoom.roomId.ToString();
        }

        #endregion

        #region Customer
        private async Task LoadCustomerAsync()
        {
            customerRepository = new CustomerRepository();
            Customers = new ObservableCollection<Customer>(await customerRepository.GetAllAsync());
        }

        #endregion

        #region Booking
        private async Task LoadBookingAsync()
        {
            bookingRepository = new BookingRepository();
            var bookings = new ObservableCollection<Booking>(await bookingRepository.GetAllAsync());
            Bookings = new ObservableCollection<Booking>(bookings.Where(b => b.checkOut == null));
            Supporter.GetControls<Button>(view, "btnPayment").Click += payment_Click;
        }

        private void payment_Click(object sender, EventArgs e)
        {
            if (currentRoom == null)
            {
                MessageBox.Show("Vui lòng chọn phòng cần thanh toán");
                return;
            }
            currentRoom.isBooked = false;
            roomRepository.AddOrUpdate(currentRoom);
            roomRepository.SaveAsync();
            Bookings.Remove(currentBooking);
            bookingRepository.AddOrUpdate(currentBooking);
            bookingRepository.SaveAsync();
            ResetAll();
        }

        #endregion

        private void SetPayment()
        {
            currentBooking = Bookings.First(b => b.roomId == currentRoom.roomId);
            currentBooking.checkOut = DateTime.Now;
            var span = currentBooking.checkOut - currentBooking.checkIn;
            var time = span.Value.TotalDays * 24 * 60 + span.Value.TotalHours * 60 + span.Value.TotalMinutes;
            currentBooking.total = Math.Max((int)((time * currentRoom.price) / 60000) * 1000, currentRoom.price);
            currentCustomer = Customers.First(c => c.customerId == currentBooking.customerId);

            Supporter.GetControls<Label>(view, "lblRoomName").Text = String.Format($"Phòng {currentRoom.roomId}");
            Supporter.GetControls<TextBox>(view, "txbCustomerId").Text = currentCustomer.customerId;
            Supporter.GetControls<TextBox>(view, "txbName").Text = currentCustomer.name;
            Supporter.GetControls<TextBox>(view, "txbPhone").Text = currentCustomer.phone;
            Supporter.GetControls<TextBox>(view, "txbCheckIn").Text = currentBooking.checkIn.ToString("dd/MM/yyyy HH:mm:ss");
            Supporter.GetControls<TextBox>(view, "txbCheckOut").Text = currentBooking.checkOut.Value.ToString("dd/MM/yyyy HH:mm:ss");
            Supporter.GetControls<TextBox>(view, "txbRoomPrice").Text = currentRoom.price.ToString();
            Supporter.GetControls<TextBox>(view, "txbTotal").Text = currentBooking.total.ToString();
        }

        private void ResetAll()
        {
            currentBooking = null;
            currentCustomer = null;
            currentRoom = null;

            Supporter.GetControls<Label>(view, "lblRoomName").Text = "Phòng";
            Supporter.GetControls<TextBox>(view, "txbCustomerId").Text = null;
            Supporter.GetControls<TextBox>(view, "txbName").Text = null;
            Supporter.GetControls<TextBox>(view, "txbPhone").Text = null;
            Supporter.GetControls<TextBox>(view, "txbCheckIn").Text = null;
            Supporter.GetControls<TextBox>(view, "txbCheckOut").Text = null;
            Supporter.GetControls<TextBox>(view, "txbRoomPrice").Text = null;
            Supporter.GetControls<TextBox>(view, "txbTotal").Text = null;
            Supporter.GetControls<ComboBox>(view, "cbbFloor").Items.Clear();
            Supporter.GetControls<ComboBox>(view, "cbbFloor").Text = null;
            Supporter.GetControls<ComboBox>(view, "cbbRoom").Items.Clear();
            Supporter.GetControls<ComboBox>(view, "cbbRoom").Text = null;
            Supporter.GetControls<Panel>(view, "pnlRooms").Controls.Clear();
            LoadRoomAsync();
        }


    }
}

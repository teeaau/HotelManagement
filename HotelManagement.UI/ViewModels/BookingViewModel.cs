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
    public class BookingViewModel
    {
        #region Field
        BookingView view;

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
        Booking currentBooking;

        #endregion

        public BookingViewModel(BookingView view)
        {
            this.view = view;
            LoadAsync();
        }

        private async Task LoadAsync()
        {
            LoadRoomAsync();
            LoadCustomerAsync();
            LoadBookingAsync();
        }

        #region Room
        private async Task LoadRoomAsync()
        {
            roomRepository = new RoomRepository();
            var rooms = new ObservableCollection<Room>(await roomRepository.GetAllAsync());
            floorRepository = new FloorRepository();
            Floors = new ObservableCollection<Floor>(await floorRepository.GetAllAsync());
            Rooms = new ObservableCollection<Room>(rooms.Where(r => r.isBooked == false));
            foreach (var floor in Floors)
            {
                floor.Rooms = new ObservableCollection<Room>(Rooms.Where(r => r.floorId == floor.floorId));
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
            DesignRoomBooking();
        }

        private void Room_SelectedIndexChanged(object sender, EventArgs e)
        {
            var index = Supporter.GetControls<ComboBox>(view, "cbbRoom").SelectedIndex;
            currentRoom = FollowRooms[index];
            Supporter.GetControls<TextBox>(view, "txbRoomPrice").Text = currentRoom.price.ToString();
            Supporter.GetControls<Label>(view, "lblRoomName").Text = String.Format($"Phòng {currentRoom.roomId}");
        }

        private void DesignRoomBooking()
        {
            var pnlRooms = Supporter.GetControls<Panel>(view, "pnlRooms");
            pnlRooms.Controls.Clear();
            int i = 0, j = 0;
            foreach(var room in FollowRooms)
            {
                var rbooking = new RoomBookingView();
                Supporter.GetControls<Label>(rbooking, "lblRoom").Text = String.Format($"Phòng {room.roomId}");
                Supporter.GetControls<Button>(rbooking, "btnBook").Click += delegate (object sender, EventArgs e) { rbooking_Click(sender, e, room); };
                rbooking.Size = new System.Drawing.Size(150, 150);
                rbooking.Location = new System.Drawing.Point(j * 160, i * 160);
                ++j;
                i += (j / 5);
                j %= 5;
                pnlRooms.Controls.Add(rbooking);
            }
        }

        private void rbooking_Click(object sender, EventArgs e, Room room)
        {
            currentRoom = room;
            Supporter.GetControls<TextBox>(view, "txbRoomPrice").Text = currentRoom.price.ToString();
            Supporter.GetControls<Label>(view, "lblRoomName").Text = String.Format($"Phòng {currentRoom.roomId}");
            Supporter.GetControls<ComboBox>(view, "cbbRoom").Text = currentRoom.roomId.ToString();
        }

        #endregion

        #region Customer
        private async Task LoadCustomerAsync()
        {
            customerRepository = new CustomerRepository();
            Customers = new ObservableCollection<Customer>(await customerRepository.GetAllAsync());
            Supporter.GetControls<TextBox>(view, "txbCustomerId").KeyUp += CustomerId_KeyUp;
        }

        private void CustomerId_KeyUp(object sender, KeyEventArgs e)
        {
            var text = Supporter.GetControls<TextBox>(view, "txbCustomerId").Text;
            currentCustomer = Customers.FirstOrDefault(c => c.customerId == text);
            if (currentCustomer == null)
            {
                Supporter.GetControls<TextBox>(view, "txbName").Text = null;
                Supporter.GetControls<TextBox>(view, "txbPhone").Text = null;
                return;
            }
            Supporter.GetControls<TextBox>(view, "txbName").Text = currentCustomer.name;
            Supporter.GetControls<TextBox>(view, "txbPhone").Text = currentCustomer.phone;
        }

        #endregion

        #region Booking
        private async Task LoadBookingAsync()
        {
            bookingRepository = new BookingRepository();
            Supporter.GetControls<Button>(view, "btnBook").Click += book_Click;
        }

        private void book_Click(object sender, EventArgs e)
        {
            currentCustomer = new Customer()
            {
                customerId = Supporter.GetControls<TextBox>(view, "txbCustomerId").Text,
                name = Supporter.GetControls<TextBox>(view, "txbName").Text,
                phone = Supporter.GetControls<TextBox>(view, "txbPhone").Text
            };
            if(String.IsNullOrEmpty(currentCustomer.name) || String.IsNullOrEmpty(currentCustomer.phone))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin khách hàng");
                return;
            }
            if(currentRoom == null)
            {
                MessageBox.Show("Vui lòng chọn phòng cần đặt");
                return;
            }
            customerRepository.AddOrUpdate(currentCustomer);
            customerRepository.SaveAsync();

            currentRoom.isBooked = true;
            roomRepository.AddOrUpdate(currentRoom);
            roomRepository.SaveAsync();

            currentBooking = new Booking()
            {
                customerId = currentCustomer.customerId,
                checkIn = DateTime.Now,
                roomId = currentRoom.roomId
            };
            bookingRepository.Add(currentBooking);
            bookingRepository.SaveAsync();
            ResetAll();
        }

        #endregion

        private void ResetAll()
        {
            currentBooking = null;
            currentCustomer = null;
            currentRoom = null;

            Supporter.GetControls<Label>(view, "lblRoomName").Text = "Phòng";
            Supporter.GetControls<TextBox>(view, "txbCustomerId").Text = null;
            Supporter.GetControls<TextBox>(view, "txbName").Text = null;
            Supporter.GetControls<TextBox>(view, "txbPhone").Text = null;
            Supporter.GetControls<TextBox>(view, "txbRoomPrice").Text = null;
            Supporter.GetControls<ComboBox>(view, "cbbFloor").Items.Clear();
            Supporter.GetControls<ComboBox>(view, "cbbFloor").Text = null;
            Supporter.GetControls<ComboBox>(view, "cbbRoom").Items.Clear();
            Supporter.GetControls<ComboBox>(view, "cbbRoom").Text = null;
            Supporter.GetControls<Panel>(view, "pnlRooms").Controls.Clear();
            LoadRoomAsync();
        }

    }
}
